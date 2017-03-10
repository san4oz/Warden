using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Warden.Mvc.App_Start;

namespace Warden.Mvc
{
    public class Startup : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Warden.Mvc.Controllers");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            AutofacConfig.Configure();
        }
    }
}
