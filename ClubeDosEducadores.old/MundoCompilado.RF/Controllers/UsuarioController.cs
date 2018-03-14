using MundoCompilado.RF.Attributes;
using System.Web.Mvc;

namespace MundoCompilado.RF.Controllers
{
    public class UsuarioController : FrontController
    {
        [Secure]
        public JsonResult Get()
        {
            return Json(User);
        }
    }
}