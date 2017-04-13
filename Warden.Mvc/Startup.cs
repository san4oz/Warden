using Autofac.Integration.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Core;
using Warden.Mvc.App_Start;
using Warden.Mvc.App_Start.Routes;

namespace Warden.Mvc
{
    public class Startup : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.DefaultNamespaces.Add("Warden.Mvc.Controllers");
            FrontendRouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.Configure();
            TransactionImportTracer.Configurate(WebConfigurationManager.AppSettings["TransactionImportLogsFolder"]);

            InitializeImportTasks();


        }

        private void InitializeImportTasks()
        {
            var transactionImportTask = AutofacDependencyResolver.Current.GetService<ITransactionImportTask>();
            transactionImportTask.Initialize();
        }
    }
}
