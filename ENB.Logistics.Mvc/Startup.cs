using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENB.Logistics.Mvc.Startup))]
namespace ENB.Logistics.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
