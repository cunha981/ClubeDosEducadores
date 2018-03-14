using Domain;
using Model.Enums;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.EventoVMs;
using ViewModel.Filters;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class EventoController : FrontController
    {
        private EventoDomain _domain;

        public EventoController(EventoDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(TipoEventoEnum id)
        {
            ViewBag.TipoEvento = id;
            var pagedList = _domain.Get().ToPagedList(id != TipoEventoEnum.Indefinido ? new EventoListFilter { TipoEvento = id } : null);
            return View(pagedList);
        }

        public ActionResult Get(int id)
        {
            var vm = _domain.Get(id).ToEditVM();
            return View(vm);
        }
    }
}