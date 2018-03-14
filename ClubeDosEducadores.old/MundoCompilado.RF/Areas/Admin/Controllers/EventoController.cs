using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using PagedList;
using System.Web.Mvc;
using ViewModel.EventoVMs;
using ViewModel.Filters;

namespace MundoCompilado.RF.Areas.Admin.Controllers
{
    [Secure(IsAdmin = true)]
    public class EventoController : Controller
    {
        private EventoDomain _domain;

        public EventoController(EventoDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(EventoListFilter filter)
        {
            ViewBag.Filter = filter;
            return View(_domain.Get().ToPagedList(filter));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _domain.Save(vm);
                this.AlertSuccess();

                return RedirectToAction("Index");
            }

            this.AlertError();
            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _domain.Get(id).ToEditVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventoEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _domain.Save(vm);
                this.AlertSuccess();

                return RedirectToAction("Index");
            }

            this.AlertError();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _domain.Remove(id);
            this.AlertSuccess();
            return RedirectToAction("Index");
        }

    }
}
