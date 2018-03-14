using Domain.Auth;
using Model;
using System.Web;
using System.Web.Mvc;
using ViewModel.UsuarioVMs;

namespace MundoCompilado.RF.App_Start
{
    public class FuncionarioProvider : IFuncionarioProvider
    {
        public static readonly string SESSION_USER_NAME = "UserOnline";

        public FuncionarioOnline User
        {
            get
            {
                var httpContext = DependencyResolver.Current.GetService<HttpContextBase>();
                return (FuncionarioOnline)httpContext.Session[SESSION_USER_NAME];
            }
        }
    }
}