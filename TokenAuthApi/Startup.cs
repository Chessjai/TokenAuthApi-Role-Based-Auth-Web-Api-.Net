using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using TokenAuthApi.Provider;

[assembly: OwinStartup(typeof(TokenAuthApi.Startup))]

namespace TokenAuthApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                //http and https
                AllowInsecureHttp = true,
                TokenEndpointPath=new PathString("/token"),//htttps://localhost:1000/token
                AccessTokenExpireTimeSpan=TimeSpan.FromMinutes(30),
                Provider=new AppAuthorizationServerProvider()

            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
        }
    }
}
