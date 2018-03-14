using Helper.CacheHelper;
using Model;
using System.Linq;
using System.Web.Mvc;

namespace Domain
{
    public class CargoDomain : ModelDomain<Cargo>
    {
        public SelectList GetSelectList()
        {
            return _cacheManager.Get("cargoSelectList", 120, () =>
            {
                return new SelectList(Get().OrderBy(a => a.Descricao).ToArray(), "Id", "Abreviacao");
            });
        }
    }
}