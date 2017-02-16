using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPGMMS.Startup))]
namespace IPGMMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
