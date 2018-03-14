using Helper.CacheHelper;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ViewModel.UnidadeVMs;

namespace Domain
{
    public class UnidadeDomain : ModelDomain<Unidade>
    {
        public void GetUnidadesProximas<TViewModel>(int funcionarioId, int raio, IEnumerable<int> tipos = null, string filtro = null)
        {



            //var query = "";

            //return _context.Database.SqlQuery<TViewModel>("");
        }

        public IEnumerable<UnidadeOptionVM> ToOptions()
        {
            return _cacheManager.Get("unidadeOptions", 600, () =>
            {
                return Get().ToOptions().ToArray();
            });
        }

        public IEnumerable GetUnidadesMap()
        {
            return _cacheManager.Get("unidadesMap", 120, () =>
            {
                return Get()
                        .Select(a => new { Latitude = a.Endereco.Latitude, Longitude = a.Endereco.Longitude })
                        .Where(a => a.Latitude.HasValue)
                        .ToArray();
            });
        }

        public IEnumerable<Unidade> GetCache()
        {
            return _cacheManager.Get("unidades", 600, () =>
            {
                return Get().Include(a => a.Endereco).ToArray();
            });
        }
    }
}
