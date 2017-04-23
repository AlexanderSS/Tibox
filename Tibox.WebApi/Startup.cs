using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Tibox.WebApi.Middleware;
using System.Security.Claims;
using System.Collections.Generic;
using Tibox.UnitOfWork;
using Thinktecture.IdentityModel.Owin;
using TIBox.WebApi;

[assembly: OwinStartup(typeof(Tibox.WebApi.Startup))]

namespace Tibox.WebApi
{
    public class Startup
    {

        private readonly IUnitOfWork _unit;

        public Startup()
        {
            _unit = new TiboxUnitOfWork();
        }
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);

            app.UseBasicAuthentication(
                new BasicAuthenticationOptions("TiboxSecure",
                async (username, password) => await Authenticate(username, password))
                );

            app.UseWebApi(configuration);
        }

        private async Task<IEnumerable<Claim>> Authenticate(string username, string password)
        {
            var user = _unit.Users.ValidateUser(username, password);

            if (user == null) return null;

            return new List<Claim>
            {
                new Claim("name",user.Email)
            };
        }
    }
}
