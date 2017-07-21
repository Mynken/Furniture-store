using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MebleShop.Startup))]
namespace MebleShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
