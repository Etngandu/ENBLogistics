using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Repositories.EF;
using ENB.Test.Logistics.Entities.Repositories;
using AutoMapper;
using ENB.Logistics.Mvc.App_Start;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace ENB.Logistics.Mvc.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

           // builder.RegisterControllers(Assembly.GetExecutingAssembly())‌​;
            builder.RegisterType<OperatorRepository>().As<IOperatorRepository>();
            builder.RegisterType<EFUnitOfWorkFactory>().As<IUnitOfWorkFactory>();
          
            //  builder.

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LogisticsProfiles>();
                              
            });

            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess);
            //.Where(t => t.Name.EndsWith("Repository"))
            //.AsImplementedInterfaces();


            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
                
    }
}