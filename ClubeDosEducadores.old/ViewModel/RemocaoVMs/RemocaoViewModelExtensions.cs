using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Filters;
using ViewModel.UsuarioVMs;
using System.Linq.Dynamic;

namespace ViewModel.RemocaoVMs
{
    public static class RemocaoViewModelExtensions
    {
        public static IQueryable<VagaRemocaoVM> ToList(this IQueryable<VagaRemocao> models, FuncionarioOnline user, RemocaoListFilter filter = null)
        {
            if (filter != null && filter.Tipos != null && filter.Tipos.Any())
            {
                models = models.Where(a => filter.Tipos.Contains(a.Unidade.TipoUnidadeId));
            }
            var vms = models.Where(a => a.CargoId == user.CargoId && a.Data.Year == DateTime.Now.Year).ToVagaRemocaoVM(user);

            if (filter != null)
            {
                if (filter.DificilAcessos != null && filter.DificilAcessos.Any(a => a.HasValue))
                {
                    if (filter.DificilAcessos.Contains(0))
                    {
                        vms = vms.Where(a => !a.DificilAcesso.HasValue || filter.DificilAcessos.Contains(a.DificilAcesso.Value));
                    }
                    else
                    {
                        vms = vms.Where(a => filter.DificilAcessos.Contains(a.DificilAcesso.Value));
                    }
                }

                if (filter.Distancia.HasValue)
                {
                    vms = vms.Where(a => a.Distancia <= filter.Distancia);
                }
            }

            return vms.OrderBy(filter?.Ordenacao ?? "Distancia").Take(filter?.Linhas ?? 10);
        }

        public static IQueryable<VagaRemocaoVM> ToVagaRemocaoVM(this IQueryable<VagaRemocao> models, FuncionarioOnline user)
        {
            return models.Select(a => new VagaRemocaoVM
            {
                CargoId = a.CargoId,
                DificilAcesso = a.Unidade.DificilAcesso,
                Endereco = a.Unidade.Endereco.EnderecoCompleto,
                UnidadeId = a.UnidadeId,
                Id = a.Id,
                Unidade = a.Unidade.Nome,
                Distancia =
                        (
                            SqlFunctions.Acos(
                                SqlFunctions.Sin(SqlFunctions.Radians(a.Unidade.Endereco.Latitude)) * SqlFunctions.Sin(SqlFunctions.Radians(user.Latitude))
                                +
                                SqlFunctions.Cos(SqlFunctions.Radians(a.Unidade.Endereco.Latitude)) * SqlFunctions.Cos(SqlFunctions.Radians(user.Latitude))
                                *
                                SqlFunctions.Cos(SqlFunctions.Radians(a.Unidade.Endereco.Longitude) - SqlFunctions.Radians(user.Longitude))
                            ) * 6378
                        )
            });
        }

        public static async Task<List<VagaRemocaoVM>> ToListAsync(this IQueryable<VagaRemocao> models, FuncionarioOnline user, RemocaoListFilter filter = null)
        {
            return await models.ToList(user, filter).ToListAsync();
        }

        public static VagaRemocaoVM ToVM(this IQueryable<VagaRemocao> singleModel, FuncionarioOnline user)
        {

            return singleModel.Select(a => new VagaRemocaoVM
            {
                CargoId = a.CargoId,
                DificilAcesso = a.Unidade.DificilAcesso,
                Endereco = a.Unidade.Endereco.EnderecoCompleto,
                UnidadeId = a.UnidadeId,
                Id = a.Id,
                Unidade = a.Unidade.Nome,
                Distancia =
                        (
                            SqlFunctions.Acos(
                                SqlFunctions.Sin(SqlFunctions.Radians(a.Unidade.Endereco.Latitude)) * SqlFunctions.Sin(SqlFunctions.Radians(user.Latitude))
                                +
                                SqlFunctions.Cos(SqlFunctions.Radians(a.Unidade.Endereco.Latitude)) * SqlFunctions.Cos(SqlFunctions.Radians(user.Latitude))
                                *
                                SqlFunctions.Cos(SqlFunctions.Radians(a.Unidade.Endereco.Longitude) - SqlFunctions.Radians(user.Longitude))
                            ) * 6378
                        )
            }).Single();
        }

        public static IQueryable<HistoricoRemocaoListVM> ListHistory(this IQueryable<Remocao> models)
        {
            return models.GroupBy(a => new { Data = DbFunctions.TruncateTime(a.Data), a.VagaRemocao.Cargo }).Select(a => new HistoricoRemocaoListVM
            {
                Cargo = a.Key.Cargo.Abreviacao,
                Data = a.Key.Data.Value,
                UnidadesCount = a.Count()
            });
        }

        public static async Task<List<VagaRemocaoVM>> ToListAsync(this IQueryable<Remocao> models, FuncionarioOnline user)
        {
            return await models.OrderBy(a => a.Preferencia).Select(a => a.VagaRemocao).ToVagaRemocaoVM(user).ToListAsync();
        }
    }
}
