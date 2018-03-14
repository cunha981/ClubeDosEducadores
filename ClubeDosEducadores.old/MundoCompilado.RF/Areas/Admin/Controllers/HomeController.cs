using Domain;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.AdminManagerVMs;

namespace MundoCompilado.RF.Areas.Admin.Controllers
{
    [Secure(IsAdmin = true)]
    public class HomeController : BackController
    {
        private UsuarioDomain _usuarioDomain;
        private PermutaDomain _permutaDomain;
        private EventoDomain _eventoDomain;
        private PerguntaFrequenteDomain _perguntaFrenquenteDomain;
        private CargoDomain _cargoDomain;

        public HomeController(UsuarioDomain usuarioDomain, PermutaDomain permutaDomain,
            EventoDomain eventoDomain, PerguntaFrequenteDomain perguntaFrequenteDomain,
            CargoDomain cargoDomain)
        {
            _usuarioDomain = usuarioDomain;
            _permutaDomain = permutaDomain;
            _eventoDomain = eventoDomain;
            _perguntaFrenquenteDomain = perguntaFrequenteDomain;
            _cargoDomain = cargoDomain;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View(new ManageVM(_usuarioDomain.Get(), _permutaDomain.Get(), 
                                        _eventoDomain.Get(), _perguntaFrenquenteDomain.Get(), _cargoDomain.Get()));
        }
    }
}