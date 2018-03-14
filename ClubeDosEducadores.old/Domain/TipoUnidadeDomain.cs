using Helper.CacheHelper;
using Model;
using System.Collections.Generic;
using System.Linq;
using ViewModel.TipoUnidadeVMs;

namespace Domain
{
    public class TipoUnidadeDomain : ModelDomain<TipoUnidade>
    {
        public IEnumerable<TipoUnidadeOptionVM> ToOptions()
        {
            return _cacheManager.Get("tipoUnidadeOptions", 600, () =>
            {
                return Get().ToOptions().ToArray();
            });
        }
    }
}
