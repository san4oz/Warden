using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Warden.Mvc.App_Start.Routes
{
    public class AdminRouteConfig : BaseAreaRegistrator
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        protected override string[] namespaces { get { return new[] { "Warden.Mvc.Controllers.Admin" }; } }
        protected override object defaultsComponets { get { return new { controller = "Home", action = "Index", id = UrlParameter.Optional }; } }

        public override void RegisterArea(AreaRegistrationContext registrationContext)
        {
            base.RegisterRoutes(registrationContext);

            MapRoute(
                name: "_TransactionList",
                url: "parsedtranscations",
                defaults: new { controller = "Transaction", action = "List" }               
            );

            MapRoute(
                name: "_Admin",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            MapRoute(
               name: "_Default",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" }
           );
        }
    }
}
