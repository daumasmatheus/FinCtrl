using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinCtrl.WebUI.Startup))]
namespace FinCtrl.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
