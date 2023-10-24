using System.Web.Mvc;
using System.Web.Routing;

namespace LitmusWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "user",
            //    url: "{controller}/{action}/{code}",
            //    defaults: new { controller = "User", action = "Edit", code = UrlParameter.Optional }
            //    );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "verifyUser",
                url: "{controller}/{action}/{userCode}/{verficationCode}",
                defaults: new { controller = "UserVerification", action = "verifyUser", userCode = UrlParameter.Optional, verficationCode = UrlParameter.Optional }
                );
        }
    }
}
