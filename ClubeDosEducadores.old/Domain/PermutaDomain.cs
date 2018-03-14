using Autofac;
using DataAccess;
using Domain.Mail;
using Helper;
using Helper.CacheHelper;
using Helper.IocHelper;
using Helper.MailHelper;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViewModel.PermutaVMs;

namespace Domain
{
    public class PermutaDomain : ModelDomain<Permuta>
    {
        public Permuta GetCurrent()
        {
            return Get().SingleOrDefault(a => a.FuncionarioId == _funcionarioProvider.User.Id && !a.DtExclusao.HasValue);
        }

        public IEnumerable<Permuta> GetCache()
        {
            return _cacheManager.Get($"permutas-cargoid-{_funcionarioProvider.User.CargoId}", 10, () =>
            {
                return Get().Where(a => a.Funcionario.CargoId == _funcionarioProvider.User.CargoId).OrderByDescending(a => a.DtPublicacao).ToArray();
            });
        }

        public Permuta Save(PermutaEditVM vm)
        {
            Permuta model;
            if (vm.Id == 0)
            {
                model = vm.ConvertTo<Permuta>();
                model.FuncionarioId = _funcionarioProvider.User.Id;
                Data.Add(model);
            }
            else
            {
                model = Get(vm.Id);
                _context.Set<PermutaRegiaoUnidade>().RemoveRange(model.Regioes);
                _context.Set<PermutaTipoUnidade>().RemoveRange(model.TiposUnidade);

                model.Inject(vm);
            }

            model.Regioes = vm.Regioes.Select(a => new PermutaRegiaoUnidade
            {
                RegiaoUnidadeId = a
            }).ToList();

            model.TiposUnidade = vm.TiposUnidade.Select(a => new PermutaTipoUnidade
            {
                TipoUnidadeId = a
            }).ToList();

            _context.SaveChanges();
            NotifyUser(model);
            return model;
        }

        public void Remove(PermutaRemoveVM vm)
        {
            var model = GetCurrent();

            if (model == null)
            {
                throw new InvalidOperationException("Permuta not found");
            }

            model.DtExclusao = DateTime.Now;
            model.MotivoExclusao = vm.MotivoExclusao;
            SaveChanges();
        }

        private void NotifyUser(Permuta model)
        {
            /*
             * Funcionario A - Funcionario B
             * FuncionarioA[Regioes|TiposUnidade] INTERESSA FuncionarioB[Regiao|TipoUnidade] 
             * FuncionarioB[Regioes|TiposUnidade] INTERESSA FuncionarioA[Regiao|TipoUnidade] 
             */

            var regioesId = model.Regioes.Select(a => a.RegiaoUnidadeId);
            var tiposUnidadeId = model.TiposUnidade.Select(a => a.TipoUnidadeId);

            var proponente = _context.Set<Funcionario>().Single(a => a.Id == model.FuncionarioId);
            var proponenteRegiaoId = proponente.UnidadeTrabalho.RegiaoUnidadeId.Value;
            var proponenteTipoUnidadeId = proponente.UnidadeTrabalho.TipoUnidadeId;
            var proponenteUnidadeTrabalhoId = proponente.UnidadeTrabalhoId;

            var pessoas = Data.Where(a => a.Funcionario.Cargo.Id == model.Funcionario.Cargo.Id && a.FuncionarioId != model.FuncionarioId && a.Funcionario.UnidadeTrabalhoId != proponenteUnidadeTrabalhoId
                                    && (regioesId.Contains(a.Funcionario.UnidadeTrabalho.RegiaoUnidadeId.Value) && tiposUnidadeId.Contains(a.Funcionario.UnidadeTrabalho.TipoUnidadeId))
                                    && (a.Regioes.Any(regiao => regiao.RegiaoUnidadeId == proponenteRegiaoId) && a.TiposUnidade.Any(tipoUnidade => tipoUnidade.TipoUnidadeId == proponenteTipoUnidadeId)))
                            .Select(permuta => new
                            {
                                Email = permuta.Funcionario.Usuario.Email,
                                Nome = permuta.Funcionario.Nome
                            }).ToArray();

            if (!pessoas.Any())
            {
                return;
            }

            var proponenteRegiao = proponente.UnidadeTrabalho.RegiaoUnidade.Regiao;
            var proponenteTipoUnidade = proponente.UnidadeTrabalho.TipoUnidade.Descricao;
            var proponenteUnidade = proponente.UnidadeTrabalho.Nome;
            var proponenteUnidadeLatitude = proponente.UnidadeTrabalho.Endereco.Latitude?.ToString().Replace(",", ".");
            var proponenteLongitude = proponente.UnidadeTrabalho.Endereco.Longitude?.ToString().Replace(",", ".");
            var proponenteEnderecoUnidade = proponente.UnidadeTrabalho.Endereco.EnderecoCompleto;

            var mailTemplate = _context.Set<MailTemplate>().Single(a => a.Key == MailTemplateEnum.NotifyNewPermuta.ToString());

            new System.Threading.Thread(() =>
            {
                using (var scope = ApplicationContainer.Container.BeginLifetimeScope())
                {
                    var mailProvider = scope.Resolve<IMailProvider>();
                    pessoas.Foreach(pessoa =>
                    {
                        mailProvider.Send(new Email
                        {
                            To = pessoa.Email,
                            Subject = mailTemplate.Subject,
                            Body = string.Format(mailTemplate.Body,
                            pessoa.Nome,
                            proponenteRegiao,
                            proponenteTipoUnidade,
                            proponenteUnidade,
                            proponenteUnidadeLatitude,
                            proponenteLongitude,
                            proponenteEnderecoUnidade)

                        });
                    });
                }
            }).Start();
        }
    }
}
