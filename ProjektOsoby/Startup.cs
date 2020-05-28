using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektOsoby.Startup))]
namespace ProjektOsoby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
