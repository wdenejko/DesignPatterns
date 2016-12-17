using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DesignPatternsProject.Startup))]
namespace DesignPatternsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
