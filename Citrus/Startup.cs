using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Citrus.Startup))]
namespace Citrus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
