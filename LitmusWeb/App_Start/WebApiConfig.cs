using System.Web.Http;
namespace LitmusWeb
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DashboardApi",
            //    routeTemplate: "api/{controller}/{unit_code}/{season_code}/{entry_date}",
            //    defaults: new { unit_code = RouteParameter.Optional, season_code = RouteParameter.Optional, entry_date = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "MultipleSimilarActionTypesApi",
                routeTemplate: "api/{controller}/{action}/{unit_code}",
                defaults: new { unit_code = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}