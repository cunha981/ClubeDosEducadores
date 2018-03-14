using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.PerguntaFrequenteVMs;

namespace MundoCompilado.RF.Areas.Admin.Controllers
{
    [Secure(IsAdmin = true)]
    public class PerguntaFrequenteController : BackController
    {
        private PerguntaFrequenteDomain _domain;

        public PerguntaFrequenteController(PerguntaFrequenteDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index()
        {
            return View(_domain.Get().ToListViewModel());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerguntaFrequenteVM vm)
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
            var vm = _domain.Get(id).ToViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PerguntaFrequenteVM vm)
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
