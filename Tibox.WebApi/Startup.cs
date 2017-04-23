
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Tibox.WebApi.Startup))]

namespace Tibox.WebApi
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            Register(configuration);
            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            ConfigureInjector(configuration);
            app.UseWebApi(configuration);
        }

    }
}
