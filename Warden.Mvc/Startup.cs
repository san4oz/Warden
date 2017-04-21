using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Warden.Business;
using Warden.Business.Helper;
using Warden.Business.Import;
using Warden.Mvc.App_Start;

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
            IoC.Init(AutofacDependencyResolver.Current);

            TransactionImportTracer.Configurate(WebConfigurationManager.AppSettings["TransactionImportLogsFolder"]);
            InitializeImportTasks();

        }

        private void InitializeImportTasks()
        {
            var transactionImportTask = AutofacDependencyResolver.Current.GetService<TransactionImportTask>();
            transactionImportTask.Initialize();
        }
    }
}
