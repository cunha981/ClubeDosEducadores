using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System.Web.Mvc;
using ViewModel.UnidadeAvaliacaoVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class UnidadeAvaliacaoController : FrontController
    {
        private UnidadeAvaliacaoDomain _domain;

        public UnidadeAvaliacaoController(UnidadeAvaliacaoDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(int id)
        {
            return View(_domain.GetAllByUnidade(id).ToListVM());
        }

        public ActionResult Get(int id)
        {
            return View(_domain.GetByUnidade(id).ToPostVM(id));
        }

        [ValidateAntiForgeryToken]
        public ActionResult Post(UnidadeAvaliacaoPostVM vm)
        {
            if(ModelState.IsValid)
            {
                _domain.Save(vm);
                this.AlertSuccess();
            }

            return View("Get", vm);
        }

    }
}