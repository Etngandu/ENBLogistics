using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using AutoMapper;
using ENB.Logistics.Mvc.App_Start;
using ENB.Test.Logistics.Entities.Repositories;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Repositories.EF;

namespace ENB.Logistics.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static IDbConnectionFactory DefaultConnectionFactory { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();


            // Use LocalDB for Entity Framework by default

            DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\MSSQLLocalDB; Integrated Security=True; MultipleActiveResultSets=True");

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerConfig.Configure();
        }

        //private void AutofacRegister()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<OperatorRepository>().As<IOperatorRepository>();
        //    builder.RegisterType<EFUnitOfWorkFactory>().As<IUnitOfWorkFactory>();
        //    //  builder.

        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile<LogisticsProfiles>();

        //    });

        //    //Create a mapper that will be used by the DI container
        //    var mapper = config.CreateMapper();
        //    builder.RegisterInstance(mapper).As<IMapper>();

        //    var container = builder.Build();

        //    //// Setting Dependency Injection Parser
        //    //DependencyResolver.SetResolver(new);
        //    //DependencyResolver.SetResolver(i)
        //}
    }
}
