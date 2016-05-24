using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminGujaratiSamaj.Startup))]
namespace AdminGujaratiSamaj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
