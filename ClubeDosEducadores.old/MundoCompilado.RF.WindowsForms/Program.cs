using Autofac;
using System;
using System.Windows.Forms;

namespace MundoCompilado.RF.WindowsForms
{
    public static class Program
    {
        private static IContainer _container;
        private static ILifetimeScope _lifeTimeScope;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterModule<Domain.ConfigModule>();
            _container = builder.Build();

            using (_lifeTimeScope = _container.BeginLifetimeScope())
            {
                Application.Run(new MainForm());
            }
        }

        public static T Get<T>(this object obj)
        {
            return _lifeTimeScope.Resolve<T>();
        }
    }
}
