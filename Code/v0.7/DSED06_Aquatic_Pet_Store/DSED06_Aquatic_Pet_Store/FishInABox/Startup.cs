using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FishInABox.Startup))]
namespace FishInABox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
