using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITISystem.Startup))]
namespace ITISystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
