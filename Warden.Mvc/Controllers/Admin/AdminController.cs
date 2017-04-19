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
