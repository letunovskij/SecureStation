using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DivisionWebGlobal.Startup))]
namespace DivisionWebGlobal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
