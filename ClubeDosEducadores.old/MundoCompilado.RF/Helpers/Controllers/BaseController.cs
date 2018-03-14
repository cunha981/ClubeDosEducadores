using Model;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace MundoCompilado.RF.Helpers.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly string _userSessionName;

        public BaseController(string userSessionName)
        {
            _userSessionName = userSessionName;
        }

        [NonAction]
        protected abstract void Login(UsuarioFuncionario usuario);

        [NonAction]
        protected virtual void Logout()
        {
            Session[_userSessionName] = null;
        }

        [NonAction]
        protected virtual void RefreshUser()
        {
            var usuarioDomain = DependencyResolver.Current.GetService<UsuarioDomain>();
            Login(usuarioDomain.Get(GetUser().Id));
        }

        [NonAction]
        public virtual void ClearSession()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }

        protected new JsonResult Json(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public abstract IUser GetUser();
    }
}