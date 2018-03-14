using Helper.CacheHelper;
using Model;
using System.Collections.Generic;
using System.Linq;
using ViewModel.RegiaoUnidadeVMs;

namespace Domain
{
    public class RegiaoUnidadeDomain : ModelDomain<RegiaoUnidade>
    {
        public IEnumerable<RegiaoUnidadeOptionVM> ToOptions()
        {
            return _cacheManager.Get("regiaoUnidadeOptions", 6000, () =>
            {
                return Get().ToOptions().ToArray();
            });
        }
    }
}
