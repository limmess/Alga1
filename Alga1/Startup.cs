using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alga1.Startup))]
namespace Alga1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
