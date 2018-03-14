using Domain;
using Helper;
using MundoCompilado.RF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Filters;
using ViewModel.TipoUnidadeVMs;
using ViewModel.UnidadeVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class UnidadeController : FrontController
    {
        private UnidadeDomain _domain;
        private UsuarioDomain _usuarioDomain;
        private readonly TipoUnidadeDomain _tipoUnidadeDomain;

        public UnidadeController(UnidadeDomain domain, UsuarioDomain usuarioDomain, TipoUnidadeDomain tipoUnidadeDomain)
        {
            _domain = domain;
            _usuarioDomain = usuarioDomain;
            _tipoUnidadeDomain = tipoUnidadeDomain;
        }

        // GET: Unidade
        public ActionResult Index(UnidadeListFilter filter)
        {
            if (!User.Latitude.HasValue || !User.Longitude.HasValue)
            {
                this.AlertInfo("Você ainda não cadastrou seu endereço!", "<a class='alert-info' href='../Funcionario'><strong>Clique aqui</strong> e cadastre seu endereço para calcularmos as distâncias!</a>");
            }

            ViewBag.Filter = filter;
            ViewBag.TipoOptions = _tipoUnidadeDomain.ToOptions();
            return View(_domain.GetCache().ToPagedList(filter, filter.Distancia.HasValue ? User : null));
        }

        public JsonResult Get(int id)
        {
            return Json(_domain.GetCache().Single(a => a.Id == id).ToGetVM(User));
        }

        public ActionResult Options()
        {
            return Json(_domain.ToOptions());
        }
    }
}