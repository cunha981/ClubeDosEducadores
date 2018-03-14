using Helper;
using Model;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using ViewModel.Filters;
using ViewModel.UnidadeAvaliacaoVMs;
using ViewModel.UnidadeVMs;
using ViewModel.UsuarioVMs;

namespace ViewModel.PermutaVMs
{
    public static class PermutaViewModelExtensions
    {
        public static IEnumerable<PermutaListVM> ToListVM(this IEnumerable<Permuta> models, FuncionarioOnline user,  PermutaListFilter filter = null)
        {
            if(filter != null)
            {
                if (filter.Tipos.Any())
                {
                    models = models.Where(a => filter.Tipos.Any(b => b == a.Funcionario.UnidadeTrabalho.TipoUnidadeId));
                }

                if (filter.Regioes.Any())
                {
                    models = models.Where(a => filter.Regioes.Any(b => b == a.Funcionario.UnidadeTrabalho.RegiaoUnidadeId));
                }
            }

            return models.Where(a => !a.DtExclusao.HasValue 
                                && a.TiposUnidade.Any(b => b.TipoUnidadeId == user.TipoUnidadeId) 
                                && a.Regioes.Any(b => b.RegiaoUnidadeId == user.RegiaoUnidadeId))
                .Select(model => new PermutaListVM
                {
                    Id = model.Id,
                    ObservacaoFuncionario = model.ObservacaoFuncionario,
                    ObservacaoPermuta = model.ObservacaoPermuta,
                    DtPublicacao = model.DtPublicacao,
                    Regiao = model.Funcionario.UnidadeTrabalho.RegiaoUnidade.Regiao,
                    Unidade = model.Funcionario.UnidadeTrabalho.Nome
                }).ToList();
        }

        public static IPagedList<PermutaListVM> ToPagedList(this IEnumerable<Permuta> models, FuncionarioOnline user, PermutaListFilter filter = null)
        {
            return models.ToListVM(user, filter)
                .OrderByDescending(a => a.DtPublicacao)
                .ToPagedList(filter?.Page ?? 1, 10);
        }

        public static PermutaIndexVM ToIndexView(this IEnumerable<Permuta> models, FuncionarioOnline user, PermutaListFilter filter = null)
        {
            var current = models.SingleOrDefault(a => a.FuncionarioId == user.Id && !a.DtExclusao.HasValue);
            var pagedList = models.Where(a => a.Funcionario.CargoId == user.CargoId).ToPagedList(user, filter);

            return new PermutaIndexVM
            {
                Permuta = current.ToGetVM(),
                Permutas = pagedList
            };
        }

        public static PermutaGetVM ToGetVM(this Permuta model, FuncionarioOnline user = null)
        {
            var vm = model.ConvertTo<PermutaGetVM>();
            if (model != null)
            {
                vm.FuncionarioNome = model.Funcionario.Nome;
                vm.FuncionarioEmail = model.Funcionario.Usuario.Email;
                vm.FuncionarioTelefone = model.Funcionario.Telefone;
                vm.FuncionarioCelular = model.Funcionario.Celular;

                vm.Unidade = model.Funcionario.UnidadeTrabalho.ToGetVM(user);

                vm.Regioes = model.Regioes.Select(a => a.RegiaoUnidade.Regiao);
                vm.TiposUnidade = model.TiposUnidade.Select(a => a.TipoUnidade.Descricao);

                vm.Avaliacoes = model.Funcionario.UnidadeTrabalho.Avaliacoes.ToListVM();
            }

            return vm;
        }

        public static PermutaEditVM ToEditVM(this Permuta model)
        {
            var vm = model.ConvertTo<PermutaEditVM>();

            if (model != null)
            {
                if (model.Regioes != null)
                {
                    vm.Regioes = model.Regioes.Select(a => a.RegiaoUnidadeId);
                }

                if (model.TiposUnidade != null)
                {
                    vm.TiposUnidade = model.TiposUnidade.Select(a => a.TipoUnidadeId);
                }
            }

            return vm;
        }
    }
}
