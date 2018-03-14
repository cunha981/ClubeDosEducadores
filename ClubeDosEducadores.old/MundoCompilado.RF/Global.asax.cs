using Autofac;
using Autofac.Integration.Mvc;
using Domain.Auth;
using FluentValidation.Mvc;
using Helper.IocHelper;
using MundoCompilado.RF.App_Start;
using MundoCompilado.RF.App_Start.ModelBinders;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MundoCompilado.RF
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentValidationModelValidatorProvider.Configure();

            ModelBinders.Binders.Add(typeof(int), new IntModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterSource(new ViewRegistrationSource());
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerLifetimeScope();
            builder.RegisterFilterProvider();

            builder.RegisterModule<ViewModel.ConfigModule>();
            builder.RegisterModule<Domain.ConfigModule>();

            builder.RegisterType<FuncionarioProvider>().As<IFuncionarioProvider>().InstancePerRequest();
            builder.RegisterType<AdminProvider>().As<IAdminProvider>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ApplicationContainer.Container = container;
        }
    }
}
