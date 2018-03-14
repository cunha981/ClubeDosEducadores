using Autofac;
using FluentValidation;
using System.Linq;

namespace ViewModel
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                   .Where(t => t.Name.EndsWith("Validator"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();


            builder
            .RegisterType<FluentValidation.Mvc.FluentValidationModelValidatorProvider>()
            .As<System.Web.Mvc.ModelValidatorProvider>();

            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
        }
    }
}
