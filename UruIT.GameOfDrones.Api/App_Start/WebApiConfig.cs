using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using UruIT.GameOfDrones.Configuration.GlobalConfigurations;

namespace UruIT.GameOfDrones.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            new AutoMapperSetup().RegisterMapping();
            IContainer container = new AutoFacSetup().RegisterDependencies(Assembly.GetExecutingAssembly());
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
