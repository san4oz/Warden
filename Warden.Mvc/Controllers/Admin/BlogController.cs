using System.Web.Mvc;
using Warden.Mvc.Models.Admin;

namespace Warden.Mvc.Controllers.Admin
{
    public class BlogController : Controller
    {
        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            return Json(true);
        }
    }
}
