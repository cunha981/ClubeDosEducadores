using Domain;
using MundoCompilado.RF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.FuncionarioVMs;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class RestritoController : FrontController
    {
        private FuncionarioDomain _funcionarioDomain;
        private RemocaoDomain _remocaoDomain;

        public RestritoController(FuncionarioDomain funcionarioDomain, RemocaoDomain remocaoDomain)
        {
            _funcionarioDomain = funcionarioDomain;
            _remocaoDomain = remocaoDomain;
        }

        // GET: Restrito
        public ActionResult Index()
        {
            if(User.Email != "jeanjnx@gmail.com")
            {
                View();
            }

            return View(_funcionarioDomain.Get().ToListControlVM());
        }

        public ActionResult Indicacao(int id)
        {
            if (User.Email != "jeanjnx@gmail.com")
            {
                View();
            }

            ViewBag.FuncionarioId = id;

            return View(_remocaoDomain.GetIndicacaoRemocao(id));
        }

        public ActionResult IndicacaoCompleta(int id)
        {
            if (User.Email != "jeanjnx@gmail.com")
            {
                View();
            }

            ViewBag.FuncionarioId = id;

            return View(_remocaoDomain.GetIndicacaoRemocaoCompleta(id));
        }

        public ActionResult IndicacaoPrecario(int id)
        {
            if (User.Email != "jeanjnx@gmail.com")
            {
                View();
            }

            ViewBag.FuncionarioId = id;

            return View("Indicacao",_remocaoDomain.GetIndicacaoRemocaoPrecario(id));
        }
    }
}