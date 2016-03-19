using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeuSite.Startup))]
namespace MeuSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
