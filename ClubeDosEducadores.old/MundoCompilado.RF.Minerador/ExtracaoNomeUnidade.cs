using Autofac;
using Domain;
using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MundoCompilado.RF.Minerador
{
    class ExtracaoNomeUnidade
    {
        #region Configuracao do Autofac para Injecao de dependencia (DI) e Inversao de Controle (IOC)
        private static IContainer _container;
        private static ILifetimeScope _lifeTimeScope;

        static ExtracaoNomeUnidade()
        {
            //Configura autofac para usarmos Injecao de dependencia e inversao de controle
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConfigModule>();
            _container = builder.Build();

            _lifeTimeScope = _container.BeginLifetimeScope();
        }
        #endregion

        static UnidadeDomain _domain;
        static TipoUnidadeDomain _tipoUnidadeDomain;

        static void Main(string[] args)
        {
            _domain = _lifeTimeScope.Resolve<UnidadeDomain>();
            _tipoUnidadeDomain = _lifeTimeScope.Resolve<TipoUnidadeDomain>();

            //SaveJsonData();
            //UpdateDataBase();
            InsertToDataBase();
        }

        static void SaveJsonData()
        {
            for (int i = 0; i < 10; i++)
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("PageSize: 500");
                    wc.Headers.Add($"CurrentPage: {i}");

                    var json = wc.DownloadString("http://portal.sme.prefeitura.sp.gov.br///School/List?AdministrativeUnitSuperiorId=&AdministrativeUnitTypeId=&port=:80&schoolName=");

                    using (var txt = new StreamWriter($"ExtracaoNomeUnidade-{i}.json", false, Encoding.UTF8))
                    {
                        txt.Write(json);
                    }
                }
            }

        }

        static void UpdateDataBase()
        {
            var typesIgnore = new[] { "CCA", "CCI/CIPS", "CEI INDIR", "CR.P.CONV", "E TECNICA", "ESP CONV", "MOVA" };

            for (int i = 0; i < 10; i++)
            {
                using (var txt = new StreamReader($"ExtracaoNomeUnidade-{i}.json"))
                {
                    string json = txt.ReadToEnd();
                    List<UnidadeEnsino> items = JsonConvert.DeserializeObject<List<UnidadeEnsino>>(json);

                    foreach (var item in items.Where(a => !typesIgnore.Any(b => a.AdministrativeUnitType.Name.Equals(b, StringComparison.InvariantCultureIgnoreCase))))
                    {
                        var tipoUnidade = item.AdministrativeUnitType.Name.ToUpper().Replace(" DIRET", "");
                        tipoUnidade = tipoUnidade == "CEU AT COMPL" ? "CEU" : tipoUnidade.Replace("CEU ", "");

                        var models = _domain.Get().ToArray().Where(a => (a.Codigo == item.Code || a.Nome.ReplaceDiacritics().Contains(item.Name)
                                                                            || (a.NomeDiarioOficial != null && a.NomeDiarioOficial.ReplaceDiacritics().Contains(item.Name))
                                                                        )
                                                                        && a.TipoUnidade.Tipo.Equals(tipoUnidade)
                                                                    );
                        if (models.Count() != 1)
                        {
                            if (models.Count() == 0)
                            {
                                LogNotFind(item);
                                continue;
                            }

                            models = models.Where(a => a.TipoUnidade.Tipo.ToUpper() == tipoUnidade);
                            if (models.Count() != 1)
                            {
                                var name = item.AdministrativeUnitType.Name + " " + item.Name;
                                models = models.Where(a => a.Nome == item.Name || a.NomeDiarioOficial == item.Name || a.Nome == name);
                                if (models.Count() != 1)
                                {
                                    LogNotFind(item);
                                    continue;
                                }
                            }
                        }

                        var model = models.Single();

                        if (model.Codigo != null)
                        {
                            continue;
                        }

                        model = _domain.Get(model.Id);

                        model.Codigo = item.Code;
                        model.Url = item.UrlMoreInfo;
                        model.Email = item.Email;

                        if (!string.IsNullOrWhiteSpace(item.Phone))
                        {
                            var phone = $"{item.Phone.Substring(0, 4)}-{item.Phone.Substring(4, 4)}";

                            if (model.TelefonePrincipal == null || !model.TelefonePrincipal.Equals(phone))
                            {
                                model.TelefoneSecundario = model.TelefonePrincipal;
                                model.TelefonePrincipal = phone;
                            }
                        }

                        _domain.Save(model);
                    }
                }
            }
        }

        static void InsertToDataBase()
        {
            var unidadesNaoEncontradas = new string[]{
                "CEU EMEI APPARECIDO DOMINGUES, PROF."
            };

            var typesIgnore = new[] { "CCA", "CCI/CIPS", "CEI INDIR", "CR.P.CONV", "E TECNICA", "ESP CONV", "MOVA" };

            for (int i = 0; i < 10; i++)
            {
                using (var txt = new StreamReader($"ExtracaoNomeUnidade-{i}.json"))
                {
                    string json = txt.ReadToEnd();
                    List<UnidadeEnsino> items = JsonConvert.DeserializeObject<List<UnidadeEnsino>>(json);

                    foreach (var item in items.Where(a => !typesIgnore.Any(b => a.AdministrativeUnitType.Name.Equals(b, StringComparison.InvariantCultureIgnoreCase))))
                    {
                        var tipoUnidade = item.AdministrativeUnitType.Name.ToUpper();

                        var tipoUnidadeBuscaBanco = item.AdministrativeUnitType.Name.ToUpper().Replace(" DIRET", "");
                        tipoUnidadeBuscaBanco = tipoUnidadeBuscaBanco == "CEU AT COMPL" ? "CEU" : tipoUnidadeBuscaBanco.Replace("CEU ", "");

                        var nomeFormatado = tipoUnidade + " " + item.Name;

                        if (!unidadesNaoEncontradas.Any(a => a == nomeFormatado))
                        {
                            continue;
                        }

                        //LogNotFindOnlyName(item);
                        //continue;

                        if (_domain.Get().Any(a => a.NomeDiarioOficial == nomeFormatado))
                        {
                            continue;
                        }

                        var tipoUnidadeId = _tipoUnidadeDomain.Get().Single(a => a.Tipo == tipoUnidadeBuscaBanco).Id;
                        var regiaoUnidadeId = _domain.Get().Single(a => a.NomeDiarioOficial == item.AdministrativeSuperior.uad_nome).RegiaoUnidadeId;

                        var model = new Model.Unidade
                        {
                            Id = _domain.Get().OrderByDescending(a => a.Id).Select(a => a.Id).First() + 1,
                            Codigo = item.Code,
                            Url = item.UrlMoreInfo,
                            Email = item.Email,
                            Nome = nomeFormatado,
                            NomeDiarioOficial = nomeFormatado,
                            TipoUnidadeId = tipoUnidadeId,
                            RegiaoUnidadeId = regiaoUnidadeId,
                            Endereco = item.Address == null ? null : new Model.EnderecoUnidade
                            {
                                EnderecoCompleto = $"{item.Address.end_logradouro}, {item.Uae_numero.Trim()} - {item.Address.end_bairro}, Sâo Paulo - SP, {item.Address.end_cep}",
                                Latitude = double.Parse(item.Latitude),
                                Longitude = double.Parse(item.Longitude)
                            }
                        };

                        if (!string.IsNullOrWhiteSpace(item.Phone))
                        {
                            var phone = $"{item.Phone.Substring(0, 4)}-{item.Phone.Substring(4, 4)}";
                            model.TelefonePrincipal = phone;
                        }

                        using (var insertTxt = new StreamWriter("ExtracaoNomeUnidade-Insert.json", true))
                        {
                            insertTxt.WriteLine($@"
                                INSERT INTO[dbo].[Unidade] ([Id],[Nome],[Codigo],[TelefonePrincipal],[TipoUnidadeId],[Email],[NomeDiarioOficial],[RegiaoUnidadeId],[Url])
                                     VALUES
                                           ((select top 1 id + 1 from Unidade order by Id desc)
                                           ,'{model.Nome}'
                                           ,'{model.Codigo}'
                                           ,'{model.TelefonePrincipal}'
                                           ,{model.TipoUnidadeId}
                                           ,'{model.Email}'
                                           ,'{model.NomeDiarioOficial}'
                                           ,{model.RegiaoUnidadeId}
                                           ,'{model.Url}');
                            ");

                            if (model.Endereco != null)
                            {
                                insertTxt.WriteLine($@"INSERT INTO[dbo].[EnderecoUnidade]
                                    ([Id],[EnderecoCompleto],[Latitude],[Longitude]) VALUES
                                      ((select top 1 id from Unidade order by Id desc)
                                      ,'{model.Endereco.EnderecoCompleto}'
                                       ,{ model.Endereco.Latitude}
                                       ,{model.Endereco.Longitude});
                                ");
                            }

                        }

                    }
                }
            }
        }

        static void LogNotFind(UnidadeEnsino ue)
        {
            using (var txt = new StreamWriter("ExtracaoNomeUnidade-NotFind.json", true))
            {
                txt.WriteLine($"update unidade set 	Codigo = '{ue.Code}',	Email = '{ue.Email}', Url = '{ue.UrlMoreInfo}' where id = X -- {ue.Name} / {ue.AdministrativeUnitType.Name}");
            }
        }

        static void LogNotFindOnlyName(UnidadeEnsino ue)
        {
            using (var txt = new StreamWriter("ExtracaoNomeUnidade-NotFindNames.json", true))
            {
                txt.WriteLine($"{ue.AdministrativeUnitType.Name} {ue.Name}");
            }
        }
    }

    #region Class
    class UnidadeEnsino
    {
        public string UrlMoreInfo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Uae_numero { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Address Address { get; set; }
        public AdministrativeSuperior AdministrativeSuperior { get; set; }
        public AdministrativeUnitType AdministrativeUnitType { get; set; }
    }

    class AdministrativeSuperior
    {
        public string uad_nome { get; set; }
    }

    class AdministrativeUnitType
    {
        public string Name { get; set; }
    }

    class Address
    {
        public string end_cep { get; set; }
        public string end_logradouro { get; set; }
        public string end_bairro { get; set; }
        public object end_endereco { get; set; }
    }
    #endregion
}