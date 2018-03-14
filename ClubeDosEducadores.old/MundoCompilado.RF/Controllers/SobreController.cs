using MundoCompilado.RF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MundoCompilado.RF.Controllers
{
    [Secure]
    public class SobreController : FrontController
    {
        // GET: Sobre
        public ActionResult Index()
        {
            return View();
        }
    }
}