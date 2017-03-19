using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Warden.Mvc.Controllers.Admin
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
