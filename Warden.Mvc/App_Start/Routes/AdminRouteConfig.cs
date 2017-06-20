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
                name: "api/payer",
                url: "api/payer",
                defaults: new { controller = "Payer", action = "Get" }
            );

            MapRoute
            (
                name: "api/payer/save",
                url: "api/payer/save",
                defaults: new { controller = "Payer", action = "Save" }
            );

            MapRoute
            (
              name: "api/payer/update",
              url: "api/payer/update",
              defaults: new { controller = "Payer", action = "Edit" }
            );

            MapRoute
            (
                name: "api/transaction/startImport",
                url: "api/transaction/startImport",
                defaults: new { controller = "TransactionImport", action = "StartImport" }
            );

            MapRoute
            (
                name: "api/import/settings/get",
                url: "api/import/settings/get",
                defaults: new { controller = "TransactionImport", action = "GetImportSettings" }
            );

            MapRoute
            (
               name: "api/import/settings/update",
               url: "api/import/settings/update",
               defaults: new { controller = "TransactionImport", action = "UpdateImportSettings" }
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
              name: "api/transaction/keywordstocalibrate",
              url: "api/transaction/keywordstocalibrate",
              defaults: new { controller = "Transaction", action = "KeywordsToCalibrate" }
           );

            MapRoute
            (
            name: "api/transaction/calibratekeywords",
            url: "api/transaction/calibratekeywords",
            defaults: new { controller = "Transaction", action = "CalibrateKeywords" }
            );

            MapRoute
           (
               name: "api/transaction/attachToCategory",
               url: "api/transaction/attachToCategory",
               defaults: new { controller = "Transaction", action = "AttachToCategory" }
           );

            MapRoute
         (
             name: "api/transaction/processcalibratedtransactions",
             url: "api/transaction/processcalibratedtransactions",
             defaults: new { controller = "Transaction", action = "ProcessCalibratedTransactions" }
         );


            MapRoute(
                name: "api/transaction-import/logs",
                url: "api/transaction-import/logs/{payerId}/",
                defaults: new {controller = "TransactionImport", action = "ImportTaskLogs" }
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
                name: "api/post/create",
                url: "api/post/create",
                defaults: new { controller = "Blog", action = "Create" }
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
