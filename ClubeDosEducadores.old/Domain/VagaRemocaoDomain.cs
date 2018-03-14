using Model;
using System.Threading;
using System.Linq;
using System;
using ViewModel.Filters;
using ViewModel.RemocaoVMs;

namespace Domain
{
    public class VagaRemocaoDomain : ModelDomain<VagaRemocao>
    {
        /// <summary>
        /// Atualizamos a quantidade de vagas por query, para agilizar este fluxo
        /// </summary>
        /// <param name="id">id da vagaRemocao existente</param>
        /// <param name="vagas">quantidade de vagas</param>
        public void SetVagas(int id, int vagas)
        {
            _context.Database.ExecuteSqlCommand(
                $"update VagaRemocao set Vagas = {vagas} where id = {id};");
            var thread = new Thread(() =>
            {
                var model = Data.Find(id);
                Reload(model);
            });
        }

        public IQueryable<VagaRemocao> GetAvaliable(int cargoId)
        {
            return base.Get().Where(a => a.CargoId == cargoId && a.Vagas > 0 && a.Data.Year == DateTime.Now.Year);
        }

        public VagaRemocao Get(int unidadeId, int cargoId)
        {
            return GetAvaliable(cargoId).Single(a => a.UnidadeId == unidadeId);
        }
    }
}
