using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Warden.Mvc.Http
{
    public class AngularHttpModule : IHttpModule
    {     
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            ProcessRequest(context);
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context == null || context.Request == null)
                return;

            if (context.Response.StatusCode != 404)
                return;

            if (Path.HasExtension(context.Request.Path))
                return;

            if (context.Request.Path.StartsWith("/api/", StringComparison.InvariantCultureIgnoreCase))
                return;
        }
    }
}
