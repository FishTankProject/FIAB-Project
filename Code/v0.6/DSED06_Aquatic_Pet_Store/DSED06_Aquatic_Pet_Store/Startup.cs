using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DSED06_Aquatic_Pet_Store.Startup))]
namespace DSED06_Aquatic_Pet_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
