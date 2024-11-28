using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VLCitas.Startup))]
namespace VLCitas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
