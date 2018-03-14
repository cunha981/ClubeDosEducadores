using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Filters;
using ViewModel.RemocaoVMs;
using ViewModel.TipoUnidadeVMs;
using ViewModel.UnidadeVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class RemocaoController : FrontController
    {
        private VagaRemocaoDomain _vagaDomain;
        private UsuarioDomain _usuarioDomain;
        private RemocaoDomain _remocaoDomain;

        public RemocaoController(VagaRemocaoDomain vagaDomain, UsuarioDomain usuarioDomain, RemocaoDomain remocaoDomain)
        {
            _vagaDomain = vagaDomain;
            _usuarioDomain = usuarioDomain;
            _remocaoDomain = remocaoDomain;
        }

        // GET: Remocao
        public ActionResult Index()
        {
            if(!User.Latitude.HasValue || !User.Longitude.HasValue)
            {
                this.AlertInfo("Atenção!", "Informe seu endereço para criar sua lista de remoção.");
                return RedirectToAction("Index", "Funcionario");
            }

            return View();
        }

        public async Task<ActionResult> List(RemocaoListFilter filter)
        {
            return Json(await _vagaDomain.GetAvaliable(User.CargoId).ToListAsync(User, filter));
        }

        public ActionResult GetUnidade(int id)
        {
            return Json(_vagaDomain.GetAvaliable(User.CargoId)
                        .Single(a => a.UnidadeId == id)
                        .ToUnidadeVagaVM(_usuarioDomain.Get(User.Id).Funcionario));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">UnidadeId</param>
        /// <returns></returns>
        public ActionResult Get(int id)
        {
            return Json(_vagaDomain.GetAvaliable(User.CargoId).Where(a => a.UnidadeId == id).ToVM(User));
        }

        public ActionResult AvaliableUnidadeOptions()
        {
            return Json(_vagaDomain.GetAvaliable(User.CargoId).Select(a => a.Unidade).ToOptions());
        }

        /// <summary>
        /// Listamos apenas os Tipos de Unidade das unidades com vagas para o cargo do usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult TipoUnidadeOptions()
        {
            return Json(_vagaDomain.GetAvaliable(User.CargoId).Select(a => a.Unidade.TipoUnidade).Distinct().ToOptions());
        }

        [HttpPost]
        public ActionResult Post(RemocoesViewModel models)
        {
            if(ModelState.IsValid)
            {
                _remocaoDomain.Save(models.Remocoes.Select(a => a.VagaId), User.Id);
                return Json(true);
            }

            return Json(ModelState.Where(a => a.Value.Errors.Any()).Select(a => a.Value.Errors));
        }

        public ActionResult Historico()
        {
            return View(_remocaoDomain.GetAll(User.Id).ListHistory());
        }

        public async Task<ActionResult> GetHistory(DateTime data)
        {
            return Json(await _remocaoDomain.Get(User.Id, data).ToListAsync(User));
        }
    }
}