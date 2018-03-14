using Domain;
using Model;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.EventoVMs;

namespace MundoCompilado.RF.Controllers
{
    public class NoticiaController : FrontController
    {
        private EventoDomain _domain;

        public NoticiaController(EventoDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(int? page)
        {
            var pagedList = _domain.Get().ToPagedList(new ViewModel.Filters.EventoListFilter { Page = page ?? 1 }, 5);
            ViewBag.NextPage = page + 1;
            return View(pagedList);
        }

        public ActionResult Get(int id)
        {
            return View(_domain.Get(id));
        }
    }
}