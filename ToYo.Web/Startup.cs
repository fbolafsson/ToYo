using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToYo.Web.Startup))]
namespace ToYo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
