using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Warden.Mvc.App_Start
{
    public static class FrontendRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "frontend/api/payer/details",
                url: "frontend/api/payer/details",
                defaults: new { controller = "Payer", action = "Details" }
            );

            routes.MapRoute(
                name: "frontend/api/payer/all",
                url: "frontend/api/payer/all",
                defaults: new { controller = "Payer", action = "All" }
            );

            routes.MapRoute
            (
                name: "angular",
                url: "{*url}",
                defaults: new {controller = "Home", action = "Index" }
            );
        }
    }
}
