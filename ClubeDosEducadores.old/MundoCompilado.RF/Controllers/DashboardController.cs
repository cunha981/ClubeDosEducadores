using Domain;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.DashboardVMs;
using ViewModel.EventoVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class DashboardController : FrontController
    {
        private PermutaDomain _permutaDomain;
        private EventoDomain _eventoDomain;

        public DashboardController(PermutaDomain permutaDomain, EventoDomain eventoDomain)
        {
            _permutaDomain = permutaDomain;
            _eventoDomain = eventoDomain;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.Noticias = _eventoDomain.GetCache().ToListVM();
            ViewBag.Cursos = _eventoDomain.GetCache(Model.Enums.TipoEventoEnum.Curso).ToListVM();

            return View(_permutaDomain.GetCache().ToVM(User));
        }
    }
}