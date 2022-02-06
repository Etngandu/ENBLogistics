using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using AutoMapper;
using ENB.Logistics.WebAPI.App_Start;
using ENB.Test.Logistics.Entities.Repositories;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Repositories.EF;

namespace ENB.Logistics.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IDbConnectionFactory DefaultConnectionFactory { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerConfig.Configure();


            // Use LocalDB for Entity Framework by default

            DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\MSSQLLocalDB; Integrated Security=True; MultipleActiveResultSets=True");
        }
    }
}
