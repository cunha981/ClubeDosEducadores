using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.Filters;
using ViewModel.PermutaVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class PermutaController : FrontController
    {
        private PermutaDomain _domain;
        private TipoUnidadeDomain _tipoUnidadeDomain;
        private RegiaoUnidadeDomain _regiaoUnidadeDomain;

        public PermutaController(PermutaDomain domain, TipoUnidadeDomain tipoUnidadeDomain, RegiaoUnidadeDomain regiaoUnidadeDomain)
        {
            _domain = domain;
            _tipoUnidadeDomain = tipoUnidadeDomain;
            _regiaoUnidadeDomain = regiaoUnidadeDomain;
        }

        public ActionResult Index(PermutaListFilter filter)
        {

            if(!User.UnidadeId.HasValue)
            {
                this.AlertError("Atenção!", "Informe a unidade onde você trabalha");
                return RedirectToAction("Index", "Funcionario");
            }

            ViewBag.Filter = filter;
            ViewBag.TipoOptions = _tipoUnidadeDomain.ToOptions();
            ViewBag.RegiaoOptions = _regiaoUnidadeDomain.ToOptions();
            return View(_domain.Get().ToIndexView(User, filter));
        }

        public ActionResult Get(int id)
        {
            return View(_domain.Get(id).ToGetVM(User));
        }

        public ActionResult Self()
        {
            ViewBag.TipoOptions = _tipoUnidadeDomain.ToOptions();
            ViewBag.RegiaoOptions = _regiaoUnidadeDomain.ToOptions();
            return View(_domain.GetCurrent().ToEditVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(PermutaEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _domain.Save(vm);
                this.AlertSuccess();
                return RedirectToAction("Index");
            }

            this.AlertError();
            ViewBag.TipoOptions = _tipoUnidadeDomain.ToOptions();
            ViewBag.RegiaoOptions = _regiaoUnidadeDomain.ToOptions();
            return View("Self", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(PermutaRemoveVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RemoveError = true;
                ViewBag.MotivoExclusao = vm.MotivoExclusao;
                this.AlertError();

                ViewBag.TipoOptions = _tipoUnidadeDomain.ToOptions();
                ViewBag.RegiaoOptions = _regiaoUnidadeDomain.ToOptions();
                return View("Self", _domain.GetCurrent().ToEditVM());
            }

            _domain.Remove(vm);
            this.AlertSuccess();
            return RedirectToAction("Index");
        }
    }
}