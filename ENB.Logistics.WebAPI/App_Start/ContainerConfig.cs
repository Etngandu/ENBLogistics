using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Repositories.EF;
using ENB.Test.Logistics.Entities.Repositories;
using AutoMapper;
using ENB.Logistics.WebAPI.App_Start;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Http;

namespace ENB.Logistics.WebAPI.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            //  builder.

            var builder = new ContainerBuilder();


            // Register your Web Repository Unitofwork.
            builder.RegisterType<OperatorRepository>().As<IOperatorRepository>();
            builder.RegisterType<EFUnitOfWorkFactory>().As<IUnitOfWorkFactory>();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LogisticsProfiles>();
                              
            });

           




            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();

            var container = builder.Build();

            

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(container);

        }
                
    }
}