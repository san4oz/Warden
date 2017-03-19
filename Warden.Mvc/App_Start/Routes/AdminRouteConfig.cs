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

            MapRoute
            (
                name: "api/payer/all",
                url: "api/payer/all",
                defaults: new { controller = "Payer", action = "All" }
            );

            MapRoute
            (
                name: "api/payer/save",
                url: "api/payer/save",
                defaults: new { controller = "Payer", action = "Save" }
            );

            MapRoute
            (
                name: "api/transaction/parse",
                url: "api/transaction/parse",
                defaults: new { controller = "Parse", action = "Transactions" }
            );

            MapRoute
            (
                name: "_angular",
                url: "{*url}",
                defaults: new { controller = "Admin", action = "Admin" }
            );
        }
    }
}
