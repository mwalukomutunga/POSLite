using Autofac;
using POSLite.App;
using POSLite.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace POSLite.Client.Classes
{
   public  class DependencyContainer
    {
        public static ILifetimeScope Container { get; set; }
        static DependencyContainer()
        {
            if (Container == null) Container = BuildContainer();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Register individual components
            builder.Register(c => new UnitOfWork(new Persistance.AppDataContext(Config.LoadOptions().Options)))
                   .As<IUnitOfWork>().InstancePerLifetimeScope();  
            builder.Register(x => new Persistance.AppDataContext(Config.LoadOptions().Options)).AsSelf();


            // Scan an assembly for components
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t =>t.Name.EndsWith("ViewModel"))
                   .AsSelf();

            var container = builder.Build();
            return container;
        }
        
    }
}
