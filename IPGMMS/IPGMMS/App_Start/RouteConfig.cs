using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IPGMMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "About",
                url: "about/{*catchall}",
                defaults: new { controller = "Home", action = "About" }
                );

            routes.MapRoute(
                name: "Contact",
                url: "contact/{*catchall}",
                defaults: new { controller = "Home", action = "Contact" }
                );

            routes.MapRoute(
                name: "FAQ",
                url: "faq/{*catchall}",
                defaults: new { controller = "Home", action = "FAQ" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CatchAll",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
                );
        }
    }
}
