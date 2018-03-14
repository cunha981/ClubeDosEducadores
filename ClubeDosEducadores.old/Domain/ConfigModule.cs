using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Reflection;
using Model;
using Helper.MailHelper;
using Domain.Mail;
using Helper.CacheHelper;

namespace Domain
{
    public class ConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccess.ConfigModule>();

            var domains = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(domains)
                .Where(x => x.Name.EndsWith("Domain") && !x.IsInterface)
                .AsImplementedInterfaces()
                .AsSelf();

            var domainsType = domains.GetTypes().Where(a => !a.IsInterface);
            RegisterGeneric(builder, domainsType, typeof(ModelDomain<>));

            builder.RegisterType<ModelDomain<Unidade>>();
            builder.RegisterType<ModelDomain<Cargo>>();
            builder.RegisterType<ModelDomain<VagaRemocao>>();
            builder.RegisterType<ModelDomain<Funcionario>>();
            builder.RegisterType<ModelDomain<UsuarioFuncionario>>();
            builder.RegisterType<ModelDomain<TipoUnidade>>();
            builder.RegisterType<ModelDomain<EnderecoUnidade>>();

            builder.RegisterType<MailProvider>().As<IMailProvider>();
            builder.RegisterType<MailTemplateProvider>().As<IMailTemplateProvider>();

            builder.RegisterType<CacheManager>().AsImplementedInterfaces().SingleInstance();
        }

        private static void RegisterGeneric(ContainerBuilder builder,
            IEnumerable<Type> types,
            Type testType)
        {
            var baseEntityTypes = types
                .Select(t => new { Type = t, BaseType = FindBaseClass(t, testType) })
                .Where(t => t.BaseType != null && !t.Type.IsGenericType);
            foreach (var baseEntityType in baseEntityTypes)
            {
                if (baseEntityType.Type.Name.Contains("Domain"))
                {
                    break;
                }

                builder.RegisterType(baseEntityType.Type).As(baseEntityType.Type.GetInterfaces());
            }
        }

        public static Type FindBaseClass(Type currentType, Type testType)
        {
            if (currentType.BaseType == null)
            {
                return null;
            }

            if (currentType.BaseType.Namespace == testType.Namespace && currentType.BaseType.Name == testType.Name)
            {
                return currentType.BaseType;
            }

            return FindBaseClass(currentType.BaseType, testType);
        }
    }
}
