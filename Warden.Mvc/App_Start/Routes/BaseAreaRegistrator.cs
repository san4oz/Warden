using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Warden.Mvc.App_Start.Routes
{
    public abstract class BaseAreaRegistrator : AreaRegistration
    {
        public void RegisterRoutes(AreaRegistrationContext registrationContext)
        {
            this.context = registrationContext;
        }

        protected virtual object defaultsComponets { get { return new { action = "Index", id = UrlParameter.Optional }; } }

        protected virtual string[] namespaces { get { return new[] { "Warden.Controllers" }; } }

        protected AreaRegistrationContext context;
    
        protected virtual void MapRoute(string name, string url, object defaults = null)
        {
            context.MapRoute(
               name[0] == '_' ? AreaName + name : name,
               AreaName.ToLower() + "/" + url,
               defaults ?? defaultsComponets,
               namespaces: namespaces
           );
        }
    }
}
