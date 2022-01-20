using System.Web.Mvc;

namespace DoAnDT.Areas.AdminB2B
{
    public class AdminB2BAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminB2B";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminB2B_default",
                "AdminB2B/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DoAnDT.Areas.AdminB2B.Controllers" }
            );
        }
    }
}