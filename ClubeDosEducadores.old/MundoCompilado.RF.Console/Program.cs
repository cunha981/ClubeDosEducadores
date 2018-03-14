using Autofac;
using Domain;
using Model;
using System;
using System.Linq;

namespace MundoCompilado.RF.LatLang
{
    class Program
    {
        private static IContainer _container;
        private static ILifetimeScope _lifeTimeScope;

        static void Main(string[] args)
        {
            //Configura autofac para usarmos Injecao de dependencia e inversao de controle
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConfigModule>();
            _container = builder.Build();

            using (_lifeTimeScope = _container.BeginLifetimeScope())
            {
                GetLatLang();
            }

        }
        static void GetLatLang()
        {
            var domain = _lifeTimeScope.Resolve<ModelDomain<EnderecoUnidade>>();

            foreach (var end in domain.Get().Where(a => !a.Latitude.HasValue || !a.Longitude.HasValue))
            {
                Console.WriteLine($"Id: {end.Id} - Obtendo coordenadas do endereço {end.EnderecoCompleto}");

                var geo = Map.Get(end.EnderecoCompleto);

                if (geo == null)
                {
                    Console.WriteLine($"{end.Id} - NÃO FOI POSSÍVEL obter as Coordenadas do endereço {end.EnderecoCompleto}");
                    continue;
                }

                end.Latitude = geo.lat;
                end.Longitude = geo.lng;

                Console.WriteLine($"Id: {end.Id} - Coordenadas registradas");
            }

            domain.SaveChanges();
        }
    }
}
