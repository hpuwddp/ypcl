using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(xz_ypcl.Startup))]
namespace xz_ypcl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
