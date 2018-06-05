using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoffeeIdentityLab26.Startup))]
namespace CoffeeIdentityLab26
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
