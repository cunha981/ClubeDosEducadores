using Domain;
using System.Web;
using System.Web.Mvc;

namespace MundoCompilado.RF.Attributes
{
    public class SecureAttribute : AuthorizeAttribute
    {

        public bool IsAdmin { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var domain = DependencyResolver.Current.GetService<UsuarioDomain>();
            var result = domain.IsAuth(IsAdmin);

            if (!result && IsAdmin)
            {
                httpContext.Response.Redirect("~/Admin/Auth/Autenticar");
            }

            return result;
        }
    }
}