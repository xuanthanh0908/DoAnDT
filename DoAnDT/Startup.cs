using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAnDT.Startup))]
namespace DoAnDT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
