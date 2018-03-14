using Domain;
using Helper;
using ViewModel.UsuarioVMs;
using System.Web.Mvc;

namespace MundoCompilado.RF.Areas.Admin.Controllers
{
    public class AuthController : BackController
    {
        private UsuarioDomain _domain;

        public AuthController(UsuarioDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Autenticar()
        {
            ClearSession();
            return View(new AdminLoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(AdminLoginVM vm)
        {
            if (ModelState.IsValid)
            {
                Login(_domain.Login(vm, Session.SessionID));
                return RedirectToAction("Index", "Home");
            }

            this.AlertError("Atenção!", "Não foi possível autenticar");
            return View(vm);
        }

        public ActionResult Sair()
        {
            Logout();
            return RedirectToAction("Autenticar");
        }
    }
}