using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.FuncionarioVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class FuncionarioController : FrontController
    {
        private readonly FuncionarioDomain _domain;
        private readonly CargoDomain _cargoDomain;

        public FuncionarioController(FuncionarioDomain domain, CargoDomain cargoDomain)
        {
            _domain = domain;
            _cargoDomain = cargoDomain;
        }

        // GET: Funcionario
        public ActionResult Index()
        {
            ViewBag.Cargos = _cargoDomain.GetSelectList();
            return View(_domain.GetByUsuarioId(User.Id).ToEditVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FuncionarioEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _domain.Save(vm);
                RefreshUser();
                this.AlertSuccess();
            }
            else
            {
                this.AlertError();
            }

            ViewBag.Cargos = _cargoDomain.GetSelectList();
            return View(vm);
        }
    }
}