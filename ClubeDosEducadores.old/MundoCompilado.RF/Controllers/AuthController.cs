using Domain;
using Helper;
using ViewModel.UsuarioVMs;
using System.Web.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using Helper.Models;
using System.Threading.Tasks;

namespace MundoCompilado.RF.Controllers
{
    public class AuthController : FrontController
    {
        private UsuarioDomain _domain;
        private CargoDomain _cargoDomain;

        public AuthController(UsuarioDomain domain, CargoDomain cargoDomain)
        {
            _domain = domain;
            _cargoDomain = cargoDomain;
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Cargos = _cargoDomain.GetSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(UsuarioCadastrarVM vm)
        {
            if (ModelState.IsValid)
            {
                if(!await CheckRecaptchaAsync())
                {
                    this.AlertError("Atenção!", "Captcha inválido");
                    ViewBag.Cargos = _cargoDomain.GetSelectList();
                    return View(vm);
                }

                Login(_domain.Cadastrar(vm, Session.SessionID));
                return RedirectToAction("Index", "Funcionario");
            }


            this.AlertError();
            ViewBag.Cargos = _cargoDomain.GetSelectList();
            return View(vm);
        }

        public ActionResult Autenticar()
        {
            ClearSession();
            return View(new UsuarioLoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(UsuarioLoginVM vm)
        {
            if (ModelState.IsValid)
            {
                Login(_domain.Login(vm, Session.SessionID));
                return RedirectToAction("Index", "Dashboard");
            }

            this.AlertError("Atenção!", "Não foi possível autenticar");
            return View(vm);
        }

        public ActionResult RecuperarSenha(string email)
        {
            if (_domain.RecoveryPassword(email))
            {
                this.AlertSuccess("", "Enviamos a senha para o seu e-mail");
            }
            else
            {
                this.AlertError("", "Cadastro não encontrado!");
            }

            return RedirectToAction("Autenticar");
        }

        public ActionResult Sair()
        {
            Logout();
            return RedirectToAction("Autenticar");
        }

        private async Task<bool> CheckRecaptchaAsync()
        {
            if(Request.Url.Host.ToLower() == "localhost")
            {
                return true;
            }

            if(Request["g-recaptcha-response"] == null)
            {
                return false;
            }


            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "secret", "6LcImRQUAAAAAD2RACEJYPhHgnagV_iNz9VxLJEo" },
                    { "response", Request["g-recaptcha-response"].ToString() },
                    {"remoteip", Request.UserHostAddress }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = System.Web.Helpers.Json.Decode<ReCaptchaGoogle>(responseString);
                return jsonResponse.success;
            }
        }
    }
}