using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CountryCityApp.Startup))]
namespace CountryCityApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
