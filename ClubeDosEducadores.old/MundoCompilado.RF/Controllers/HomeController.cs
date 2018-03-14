using Domain;
using Helper;
using System.Web.Mvc;
using ViewModel.FaleConoscoVMs;

namespace MundoCompilado.RF.Controllers
{
    public class HomeController : FrontController
    {
        private ContactDomain _contactDomain;

        public HomeController(ContactDomain contactDomain)
        {
            _contactDomain = contactDomain;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FaleConoscoPortfolioVM model)
        {
            ViewBag.ContactSection = true;
            if (ModelState.IsValid)
            {
                _contactDomain.Send(model);
                this.AlertSuccess("", "Mensagem enviada com sucesso");
            }

            return View();
        }
    }
}