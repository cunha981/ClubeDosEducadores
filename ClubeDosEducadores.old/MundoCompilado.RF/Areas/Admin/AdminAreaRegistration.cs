using System.Web.Mvc;

namespace MundoCompilado.RF.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin",
                url: "Admin/{controller}/{action}/{id}",
                namespaces: new[] { "MundoCompilado.RF.Areas.Admin.Controllers" },
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}