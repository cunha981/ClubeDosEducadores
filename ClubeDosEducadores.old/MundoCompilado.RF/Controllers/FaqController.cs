using Domain;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class FaqController : FrontController
    {
        private PerguntaFrequenteDomain _domain;

        public FaqController(PerguntaFrequenteDomain domain)
        {
            _domain = domain;
        }

        // GET: Faq
        public ActionResult Index(string text)
        {
            ViewBag.Search = text;
            return View(_domain.Search(text));
        }
    }
}