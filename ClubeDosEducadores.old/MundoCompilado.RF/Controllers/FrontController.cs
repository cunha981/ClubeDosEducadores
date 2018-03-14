using Domain.Auth;
using Model;
using MundoCompilado.RF.App_Start;
using MundoCompilado.RF.Helpers.Controllers;
using System.Web.Mvc;
using ViewModel.UsuarioVMs;

namespace MundoCompilado.RF.Controllers
{
    public abstract class FrontController : BaseController
    {
        public new FuncionarioOnline User
        {
            get
            {
                return DependencyResolver.Current.GetService<IFuncionarioProvider>().User;
            }
        }

        public FrontController() : base(FuncionarioProvider.SESSION_USER_NAME)
        {

        }

        protected override void Login(UsuarioFuncionario usuario)
        {
            var userOnline = usuario.ToFuncionarioOnline(Session.SessionID);
            Session[FuncionarioProvider.SESSION_USER_NAME] = userOnline;
        }

        public override IUser GetUser()
        {
            return User;
        }
    }
}