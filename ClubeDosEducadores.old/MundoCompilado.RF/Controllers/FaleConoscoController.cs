using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.FaleConoscoVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class FaleConoscoController : FrontController
    {
        private readonly ContactDomain _domain;

        public FaleConoscoController(ContactDomain domain)
        {
            _domain = domain;
        }

        // GET: FaleConosco
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FaleConoscoVM vm)
        {
            if(ModelState.IsValid)
            {
                _domain.Send(vm);
                this.AlertSuccess("", "Mensagem enviada com sucesso!");
                return View();
            }
            else
            {
                this.AlertError();
                return View(vm);
            }
            
        }
    }
}