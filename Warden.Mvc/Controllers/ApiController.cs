using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Warden.Mvc.Controllers
{
    public class ApiController : Controller
    {
        protected Warden.Mvc.Helpers.JsonResult ToJson(object data, string contentType = null, Encoding contentEncoding = null, bool allowGet = false)
        {
            return new Helpers.JsonResult()
            {
                Data = data,
                JsonRequestBehavior = allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet,
                ContentEncoding = contentEncoding               
            };
        }
    }
}
