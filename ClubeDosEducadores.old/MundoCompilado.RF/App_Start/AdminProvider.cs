using Domain.Auth;
using System.Web;
using System.Web.Mvc;
using ViewModel.UsuarioVMs;

namespace MundoCompilado.RF.App_Start
{
    public class AdminProvider : IAdminProvider
    {
        public static readonly string SESSION_USER_NAME = "AdminOnline";
        public AdminOnline User
        {
            get
            {
                var httpContext = DependencyResolver.Current.GetService<HttpContextBase>();
                return (AdminOnline)httpContext.Session[SESSION_USER_NAME];
            }
        }
    }
}