using Domain;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class TipoUnidadeController : FrontController
    {
        private readonly TipoUnidadeDomain _domain;

        public TipoUnidadeController(TipoUnidadeDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Options()
        {
            return Json(_domain.ToOptions());
        }
    }
}