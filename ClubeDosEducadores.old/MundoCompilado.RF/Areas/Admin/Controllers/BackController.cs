using Domain.Auth;
using Model;
using MundoCompilado.RF.App_Start;
using MundoCompilado.RF.Helpers.Controllers;
using System.Web.Mvc;
using ViewModel.UsuarioVMs;
using System;
using MundoCompilado.RF.Attributes;

namespace MundoCompilado.RF.Areas.Admin.Controllers
{
    public class BackController : BaseController
    {
        protected new AdminOnline User
        {
            get
            {
                return DependencyResolver.Current.GetService<IAdminProvider>().User;
            }
        }

        public BackController() : base(AdminProvider.SESSION_USER_NAME)
        {
        }

        protected override void Login(UsuarioFuncionario usuario)
        {
            var userOnline = usuario.ToAdminOnline(Session.SessionID);
            Session[AdminProvider.SESSION_USER_NAME] = userOnline;
        }

        public override IUser GetUser()
        {
            return User;
        }
    }
}