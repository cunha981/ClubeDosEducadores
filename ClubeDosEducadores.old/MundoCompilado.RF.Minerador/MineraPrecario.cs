using Autofac;
using Domain;
using Helper;
using Helper.RegexHelper;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MundoCompilado.RF.Minerador
{
    class MineraPrecario
    {
        #region Configuracao do Autofac para Injecao de dependencia (DI) e Inversao de Controle (IOC)
        private static IContainer _container;
        private static ILifetimeScope _lifeTimeScope;

        static MineraPrecario()
        {
            //Configura autofac para usarmos Injecao de dependencia e inversao de controle
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConfigModule>();
            _container = builder.Build();

            _lifeTimeScope = _container.BeginLifetimeScope();
        }
        #endregion

        static ModelDomain<Unidade> _unidadeDomain;
        static ModelDomain<VagaRemocao> _vagaDomain;
        static ModelDomain<Cargo> _cargoDomain;
        // System.Text.Encoding.GetEncoding(1252)
        static StreamReader _vagasTxt = new StreamReader("vagas.txt", System.Text.Encoding.UTF8);
        static StreamWriter _unidadesTxt = new StreamWriter("unidades.txt", false);
        static StreamWriter _cargosTxt = new StreamWriter("cargos.txt", false);

        static IEnumerable<Cargo> _cargos;

        static ICollection<string> _cargosList = new List<string>();
        static ICollection<string> _unidadesList = new List<string>();

        static IDictionary<int, string> _unidadeDictionary;
        static IDictionary<int, string> _unidadeDoDictionary;

        static void Main(string[] args)
        {
            _unidadeDomain = _lifeTimeScope.Resolve<ModelDomain<Unidade>>();
            _vagaDomain = _lifeTimeScope.Resolve<ModelDomain<VagaRemocao>>();
            _cargoDomain = _lifeTimeScope.Resolve<ModelDomain<Cargo>>();

            _cargos = _cargoDomain.Get().ToArray();
            _unidadeDictionary = _unidadeDomain.Get().ToDictionary(a => a.Id, a => a.Nome.ReplaceDiacritics().ToUpper());
            _unidadeDoDictionary = _unidadeDomain.Get().Where(a => a.NomeDiarioOficial != null).ToDictionary(a => a.Id, a => a.NomeDiarioOficial.ReplaceDiacritics().ToUpper());

            //Passo 1: Analisar unidades e cargos extraidos
            //ExecutarAnalise(false, true);
            //Passo 2: Testar TXT e checar pendencias negativas
            var vagas = TestarTxtFinal();
            //Passo 3: Salvar vagas - ATENÇÃO: Só executar esse passo se tiver certeza que a analise e testes estão corretos
            Processar(vagas);
        }

        /// <summary>
        /// Extracao de cargos e unidades nao encontrados no banco para TXTs
        /// Apos extracao deve ser analisado os TXTs e incluir vagas e unidades no banco de dados
        /// </summary>
        /// <param name="processar"></param>
        private static void ExecutarAnalise(bool processar = false, bool checkCargo = false)
        {
            if (!processar)
            {
                string linha;
                while ((linha = _vagasTxt.ReadLine()) != null)
                {
                    Console.ResetColor();
                    Console.WriteLine($"Analisando = {linha}");
                    Analisar(linha, checkCargo);
                }
            }

            _vagasTxt.Close();
            _unidadesTxt.Close();
            _cargosTxt.Close();
        }

        /// <summary>
        /// Salvar vagas para remocao no banco de dados
        /// </summary>
        /// <param name="vagas"></param>
        static void Processar(IEnumerable<VagaRemocao> vagas)
        {
            if(vagas == null)
            {
                throw new ArgumentNullException("vagas");
            }

            if(vagas.GroupBy(a => new {a.CargoId, a.UnidadeId, a.Data.Year }).Where(a => a.Count() > 1).Any())
            {
                throw new Exception($"Existem vagas repetidas na coleção");
            }

            //var ano = vagas.First().Data.Year;
            //if(_vagaDomain.Get().Any(a => a.Data.Year == ano))
            //{
            //    if (_vagaDomain.Get().ToArray().Any(a => vagas.Any(b => a.CargoId == b.CargoId && a.UnidadeId == b.UnidadeId && a.Data.Year == b.Data.Year)))
            //    {
            //        throw new Exception($"Existem vagas já cadastradas no banco de dados");
            //    }
            //}

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Salvando vagas em lote...");

            //_vagaDomain.Save(vagas);

            var sb = new StringBuilder();
            foreach (var v in vagas)
            {
                if (v.Id == 0)
                {
                    sb.AppendLine($@"INSERT INTO VagaRemocaoPrecario (UnidadeId,CargoId,Vagas,[Data]) VALUES ({v.UnidadeId},{v.CargoId}, {v.Vagas}, CONVERT(date,getdate()));");
                    continue;
                }

                sb.AppendLine($@"update VagaRemocaoPrecario set Vagas = {v.Vagas}, [Data] = CONVERT(date,getdate()) where id = {v.Id};");
            }

            using (var sw = new StreamWriter("script.sql", false))
            {
                sw.Write(sb.ToString());
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas Vagas foram registradas com sucesso!");
            Console.ReadLine();
        }

        #region Preparacao de txt final
        static void Analisar(string linha, bool checkCargo = true)
        {
            if (linha.StartsWithNumber(5))
            {
                if(!checkCargo) 
                {
                    return; //cargos resolvidos
                }

                var codigoCargo = linha.Substring(0, 5); //as 5 primeiras posicoes representa o codigo do cargo
                if (_cargosList.Any(a => a.Substring(0, 5) == codigoCargo)
                    || _cargoDomain.Get().Any(a => a.Codigo == codigoCargo)) 
                {
                    return;
                }

                _cargosList.Add(linha);
                _cargosTxt.WriteLine(linha);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"CARGO registrado");
                return;
            }

            if (_unidadesList.Any(a => a == linha))
            {
                return;
            }

            _unidadesList.Add(linha);
            
            if (RegistrarNomeUnidade(linha))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"UNIDADE encontrada");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                _unidadesTxt.WriteLine(linha);
                Console.WriteLine($"UNIDADE registrada");
            }
        }

        static bool RegistrarNomeUnidade(string linha)
        {
            var comparativo = linha.StartsWith("CEI DIRET ") ? linha.Replace(" DIRET ", " ") : linha;
            KeyValuePair<int, string> unidadePair;

            unidadePair = _unidadeDictionary.SingleOrDefault(a => comparativo.Equals(a.Value, StringComparison.CurrentCultureIgnoreCase));

            //procurar em nomes do DO
            if (unidadePair.Equals(default(KeyValuePair<int, string>)))
            {
                //usamos a var linha ao inves de comparativo
                //pois entende-se que o mesmo nome da linha e o que esta salvo na coluan NomeDiarioOficial
                unidadePair = _unidadeDoDictionary.SingleOrDefault(a => linha.Equals(a.Value, StringComparison.CurrentCultureIgnoreCase));
            }

            if (unidadePair.Equals(default(KeyValuePair<int, string>)))
            {
                if (!Regex.IsMatch(comparativo, ".*, (PROF|VER|GEN|DR|MAL|DES|TTE|DEP|PRES|PREF|SG|MIN|CEL|PE|ENG|PDE|SEN|CAP|CM|CTE|ALM|ARQ|SGT|BRIG|MJ|GOV|EMB|CDE)(A|.|A.|)"))
                {
                   return false;
                }

                comparativo = CorrigirNomeUnidade(comparativo);
                unidadePair = _unidadeDictionary.SingleOrDefault(a => comparativo.Equals(a.Value, StringComparison.CurrentCultureIgnoreCase));

                if (unidadePair.Equals(default(KeyValuePair<int, string>)))
                {
                    return false;
                }
            }

            var unidade = _unidadeDomain.Get(unidadePair.Key);

            if(unidade.NomeDiarioOficial != null)
            {
                return true;
            }

            unidade.NomeDiarioOficial = linha;
            _unidadeDomain.Save(unidade);
            return true;
        }

        static string CorrigirNomeUnidade(string linha)
        {
            //DEU ERRADO, PROFA RETORNOU PROF NO EXTRACT - ARRUMAR
            var abreviacao = linha.Extract(", (PROF|VER|GEN|DR|MAL|DES|TTE|DEP|PRES|PREF|SG|MIN|CEL|PE|ENG|PDE|SEN|CAP|CM|CTE|ALM|ARQ|SGT|BRIG|MJ|GOV|EMB|CDE)(A|.|A.|)").Replace(", ", "");

            switch (abreviacao)
            {
                case "PROF":
                case "PROF.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " PROFº"); //PROF. PROF
                case "PROFA":
                case "PROFA.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " PROFª"); //PROFA. PROFA
                case "DEP":
                case "DEP.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " DEPUTADO");
                case "VER":
                case "VER.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " VEREADOR");
                case "GEN":
                case "GEN.":
                case "GAL":
                case "GAL.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " GENERAL");
                case "DES":
                case "DES.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " DESEMBARGADOR");
                case "MAL":
                case "MAL.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " MARECHAL");
                case "DR":
                case "DR.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " DOUTOR");
                case "DRA":
                case "DRA.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " DOUTORA");
                case "TTE":
                case "TTE.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " TENENTE");
                case "PRES":
                case "PRES.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " PRESIDENTE");
                case "PREF":
                case "PREF.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " PREFEITO");
                case "SG":
                case "SG.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " SARGENTO");
                case "MIN":
                case "MIN.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " MINISTRO");
                case "CEL":
                case "CEL.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " CORONEL");
                case "PE":
                case "PE.":
                case "PDE":
                case "PDE.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " PADRE");
                case "ENG":
                case "ENG.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " ENGENHEIRO");
                case "SEN":
                case "SEN.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " SENADOR");
                case "CAP":
                case "CAP.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " CAPITAO");
                case "COM":
                case "COM.":
                case "CTE":
                case "CTE.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " COMANDANTE");
                case "ALM":
                case "ALM.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " ALMIRANTE");
                case "ARQ":
                case "ARQ.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " ARQUITETO");
                case "SGT":
                case "SGT.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " SARGENTO");
                case "BRIG":
                case "BRIG.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " BRIGADEIRO");
                case "MJ":
                case "MJ.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " MAJOR");
                case "GOV":
                case "GOV.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " GOVERNADOR");
                case "EMB":
                case "EMB.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " EMBAIXADOR");
                case "CDE":
                case "CDE.":
                    return linha.Replace(" " + abreviacao + (linha.EndsWith(".") && !abreviacao.EndsWith(".") ? "." : ""), " CONDE");
                default:
                    return linha;
            }
        }
        #endregion

        /// <summary>
        /// Teste e extracao de lista de vagas
        /// Apos ser realizado o teste deve ser verificado o txt negativo para corrigir se possivel
        /// as linhas nao processadas como vaga
        /// </summary>
        /// <returns></returns>
        static IEnumerable<VagaRemocao> TestarTxtFinal()
        {
            ICollection<VagaRemocao> vagasList = new List<VagaRemocao>();

            string unidade = null;
            var unidadeId = 0;

            var linha = 0;
            var valor = "";
            StreamWriter txtNegativo = new StreamWriter("resultado_negativo.txt", false);
            StreamWriter txtPositivo = new StreamWriter("resultado_positivo.txt", false);
            Console.ForegroundColor = ConsoleColor.White;
            while ((valor = _vagasTxt.ReadLine()) != null)
            {
                Console.WriteLine("Testando e reunindo vagas...");
                Console.WriteLine($"Linha {linha} esta sendo analisada...");


                linha++;
                var unidadePair = _unidadeDoDictionary.SingleOrDefault(a => a.Value == valor);

                if (unidadeId == 0 || !valor.StartsWithNumber(5))
                {
                    unidade = null; //se == 0 continua vazio, porem se nao for cargo e esta preenhido deve ser zerado
                    unidadeId = 0; //pois quer dizer que ja foi lido os cargos da ultima unidade e agora estamos em outra

                    if (unidadePair.Equals(default(KeyValuePair<int, string>)))
                    {
                        txtNegativo.WriteLine($"{linha}    {valor} - Unidade");
                        continue;
                    }

                    unidadeId = unidadePair.Key;
                    unidade = unidadePair.Value;
                    txtPositivo.WriteLine($"{linha}     {valor}");
                    continue;
                }

                var codigoCargo = valor.Substring(0, 5);

                if(codigoCargo == null)
                {
                    txtNegativo.WriteLine($"{linha}     {valor} - CodigoCargo");
                    continue;
                }

                var cargo = _cargos.SingleOrDefault(a => a.Codigo == codigoCargo);

                if (cargo == null)
                {
                    txtNegativo.WriteLine($"{linha}     {valor} - Cargo");
                    continue;
                }

                var arraySplit = valor.Split(' ');

                if(arraySplit.Length == 0)
                {
                    txtNegativo.WriteLine($"{linha}     {valor} - Split Vagas");
                    continue;
                }

                var stringVagas = arraySplit.Last();
                int vagas;
                if(!int.TryParse(stringVagas, out vagas))
                {
                    txtNegativo.WriteLine($"{linha}     {stringVagas} - Vagas");
                    continue;
                }


                if(vagasList.Any(a => a.CargoId == cargo.Id && a.UnidadeId == unidadeId))
                {
                    txtNegativo.WriteLine($"{linha}     {unidadeId}-{unidade}--{cargo.Id}-{cargo.Abreviacao}--{vagas} - VAGA REPETIDA");
                    continue;
                }

                txtPositivo.WriteLine($"{linha}     {cargo.Id}-{cargo.Abreviacao}-{vagas}");
                vagasList.Add(new VagaRemocao { CargoId = cargo.Id, UnidadeId = unidadeId, Vagas = vagas, Data = DateTime.Now.Date.AddYears(2) });
            }

            _vagasTxt.Close();
            txtNegativo.Close();
            txtPositivo.Close();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vagas coletadas");
            return vagasList;
        }
    }
}
