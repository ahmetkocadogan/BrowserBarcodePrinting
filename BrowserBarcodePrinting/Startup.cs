using Microsoft.Owin.Extensions;
using Owin;
using System.Web.Http;

namespace BrowserBarcodePrinting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                context.Response.Headers.Remove("Server");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostAcquireState);

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            //// Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "print",
                routeTemplate: "api/{controller}/{action}/{document}",
                defaults: new { document = RouteParameter.Optional }
            );

            //OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = false,
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = new SimpleAuthorizationServerProvider()
            //};

            //// Token Generation
            //app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseWebApi(config);

            //// Configure Web API for self-host. 
            //var config = new HttpConfiguration();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Nox",
            //    routeTemplate: "api/{controller}/{action}/{belge}",
            //    defaults: new { belge = RouteParameter.Optional }
            //);

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.MapHttpAttributeRoutes();
            //app.UseWebApi(config);
        }
        //public void ConfigureOAuth(IAppBuilder app)
        //{
        //    OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        //        Provider = new SimpleAuthorizationServerProvider()
        //    };

        //    // Token Generation
        //    app.UseOAuthAuthorizationServer(OAuthServerOptions);
        //    app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        //}
    }
}