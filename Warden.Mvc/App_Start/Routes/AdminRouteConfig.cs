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
                name: "api/transaction/startImport",
                url: "api/transaction/startImport",
                defaults: new { controller = "Transaction", action = "StartImport" }
            );

            MapRoute
            (
                name: "api/transaction/get",
                url: "api/transaction/get",
                defaults: new { controller = "Transaction", action = "Get" }
            );

            MapRoute
            (
                name: "api/transaction/search",
                url: "api/transaction/search",
                defaults: new { controller = "Transaction", action = "Search" }
            );

            MapRoute
            (
                name: "api/transaction/count",
                url: "api/transaction/count",
                defaults: new { controller = "Transaction", action = "Count" }
            );

            MapRoute
            (
                name: "api/transaction/processed",
                url: "api/transaction/processed",
                defaults: new { controller = "Transaction", action = "GetProcessedTransaction" }
            );

            MapRoute
            (
                name: "api/task/index",
                url: "api/task/index",
                defaults: new { controller = "Task", action = "Index" }
            );

            MapRoute
           (
               name: "api/transaction/attachToCategory",
               url: "api/transaction/attachToCategory",
               defaults: new { controller = "Transaction", action = "AttachToCategory" }
           );

            MapRoute
            (
                name: "api/category/unprocessedkeywords",
                url: "api/category/unprocessedkeywords",
                defaults: new { controller = "Category", action = "UnprocessedKeywords" }
            );

            MapRoute
            (
                name: "api/category/all",
                url: "api/category/all",
                defaults: new { controller = "Category", action = "All" }
            );

            MapRoute
           (
               name: "api/category/create",
               url: "api/category/create",
               defaults: new { controller = "Category", action = "Create" }
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
