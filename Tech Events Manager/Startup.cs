using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tech_Events_Manager.Startup))]
namespace Tech_Events_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
