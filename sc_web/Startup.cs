using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sc_web.Startup))]
namespace sc_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
