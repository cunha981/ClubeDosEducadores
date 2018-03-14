using Helper;
using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModel.Filters;
using ViewModel.UsuarioVMs;

namespace ViewModel.UnidadeVMs
{
    public static class UnidadeViewModelExtensions
    {
        public static IEnumerable<UnidadeListVM> ToListVM(this IEnumerable<Unidade> models, UnidadeListFilter filter = null, FuncionarioOnline user = null)
        {
            if (filter != null)
            {
                if (filter.Tipos.Any())
                {
                    models = models.Where(a => filter.Tipos.Any(b => b == a.TipoUnidadeId));
                }

                if (!string.IsNullOrWhiteSpace(filter.Filtro))
                {
                    models = models.Where(a => a.Nome.ToUpper().Contains(filter.Filtro.ToUpper()));
                }

                if (filter.Distancia.HasValue && user?.Latitude != null)
                {
                    models = models
                                .Where(model =>
                                        (
                                        Math.Acos(
                                        Math.Sin(model.Endereco.Latitude.Value.ToRadians()) * Math.Sin(user.Latitude.Value.ToRadians())
                                        + Math.Cos(model.Endereco.Latitude.Value.ToRadians()) * Math.Cos(user.Latitude.Value.ToRadians())
                                        * Math.Cos(model.Endereco.Longitude.Value.ToRadians() - user.Longitude.Value.ToRadians())
                                ) * 6378) < filter.Distancia);
                }
            }

            if (user != null)
            {
                return models.Select(model => new UnidadeListVM
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Tipo = model.TipoUnidade.Tipo,
                    Endereco = model.Endereco.EnderecoCompleto,
                    DificilAcesso = model.DificilAcesso,
                    Distancia =
                    (
                        Math.Acos(
                                    Math.Sin(model.Endereco.Latitude.Value.ToRadians()) * Math.Sin(user.Latitude.Value.ToRadians())
                                    + Math.Cos(model.Endereco.Latitude.Value.ToRadians()) * Math.Cos(user.Latitude.Value.ToRadians())
                                    * Math.Cos(model.Endereco.Longitude.Value.ToRadians() - user.Longitude.Value.ToRadians())
                                ) * 6378
                    )
                });
            }

            return models.Select(a => new UnidadeListVM
            {
                Id = a.Id,
                Nome = a.Nome,
                Tipo = a.TipoUnidade.Tipo,
                Endereco = a.Endereco.EnderecoCompleto,
                DificilAcesso = a.DificilAcesso
            });
        }

        public static IPagedList ToPagedList(this IEnumerable<Unidade> models, UnidadeListFilter filter = null, FuncionarioOnline user = null)
        {
            if (user == null)
            {
                return models.ToListVM(filter, user)
                .OrderByDescending(a => a.DificilAcesso)
                .ToPagedList(filter?.Page ?? 1, 10);
            }

            return models.ToListVM(filter, user)
                .OrderBy(a => a.Distancia)
                .ToPagedList(filter?.Page ?? 1, 10);
        }

        public static UnidadeGetVM ToGetVM(this Unidade model, FuncionarioOnline user = null)
        {
            var vm = model.ConvertTo<UnidadeGetVM>();
            vm.Endereco = model.Endereco.EnderecoCompleto;
            vm.Latitude = model.Endereco.Latitude;
            vm.Longitude = model.Endereco.Longitude;
            vm.Tipo = model.TipoUnidade.Tipo;
            vm.TipoDescricao = model.TipoUnidade.Descricao;

            if (model.Endereco.Latitude.HasValue && model.Endereco.Longitude.HasValue
                && user != null && user.Latitude.HasValue && user.Longitude.HasValue)
            {

                vm.Distancia = Math.Acos(
                                    Math.Sin(model.Endereco.Latitude.Value.ToRadians()) * Math.Sin(user.Latitude.Value.ToRadians())
                                    + Math.Cos(model.Endereco.Latitude.Value.ToRadians()) * Math.Cos(user.Latitude.Value.ToRadians())
                                    * Math.Cos(model.Endereco.Longitude.Value.ToRadians() - user.Longitude.Value.ToRadians())
                                ) * 6378;
            }

            return vm;
        }

        public static UnidadeVagaVM ToUnidadeVagaVM(this VagaRemocao model, Funcionario funcionario = null)
        {
            var vm = model.Unidade.ConvertTo<UnidadeVagaVM>();
            vm.Endereco = model.Unidade.Endereco.EnderecoCompleto;
            vm.Latitude = model.Unidade.Endereco.Latitude;
            vm.Longitude = model.Unidade.Endereco.Longitude;
            vm.Tipo = model.Unidade.TipoUnidade.Tipo;
            vm.TipoDescricao = model.Unidade.TipoUnidade.Descricao;
            vm.Vagas = model.Vagas;

            if (model.Unidade.Endereco.Latitude.HasValue && model.Unidade.Endereco.Longitude.HasValue
                && funcionario != null && funcionario.Latitude.HasValue && funcionario.Longitude.HasValue)
            {

                vm.Distancia = Math.Acos(
                                    Math.Sin(model.Unidade.Endereco.Latitude.Value.ToRadians()) * Math.Sin(funcionario.Latitude.Value.ToRadians())
                                    + Math.Cos(model.Unidade.Endereco.Latitude.Value.ToRadians()) * Math.Cos(funcionario.Latitude.Value.ToRadians())
                                    * Math.Cos(model.Unidade.Endereco.Longitude.Value.ToRadians() - funcionario.Longitude.Value.ToRadians())
                                ) * 6378;
            }

            return vm;
        }

        public static IQueryable<UnidadeOptionVM> ToOptions(this IQueryable<Unidade> models)
        {
            return models.Select(a => new UnidadeOptionVM
            {
                Id = a.Id,
                Nome = a.Nome
            });
        }
    }
}
