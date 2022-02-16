using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Confirm Order",
               url: "xac-nhan-don-hang.html",
               defaults: new { controller = "Home", action = "ConfirmOrder", id = UrlParameter.Optional },
               namespaces: new string[] { "DoAnDT.Controllers" }
              );
            routes.MapRoute(
               name: "Cancel Order",
               url: "huy-don-hang.html",
               defaults: new { controller = "Home", action = "CancelOrder", id = UrlParameter.Optional },
               namespaces: new string[] { "DoAnDT.Controllers" }
              );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DoAnDT.Controllers" }
            );
        }
    }
}
