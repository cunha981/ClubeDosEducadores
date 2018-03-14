using Domain;
using System.Web.Mvc;

namespace MundoCompilado.RF.Controllers
{
    public class MapController : FrontController
    {
        private UnidadeDomain _unidadeDomain;

        public MapController(UnidadeDomain unidadeDomain)
        {
            _unidadeDomain = unidadeDomain;
        }

        // GET: Map
        public ActionResult Unidades()
        {
            return Json(_unidadeDomain.GetUnidadesMap());
        }
    }
}