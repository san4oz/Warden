using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostManager postManager;
        public BlogController()
        {
            postManager = IoC.Resolve<PostManager>();
        }

        [HttpPost]
        public ActionResult Posts()
        {
            var posts = postManager.All();
            var models = posts.Select(p => CreatePostPreview(p));
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        private PostPreviewViewModel CreatePostPreview(Post post)
        {
            return new PostPreviewViewModel()
            {
                Title = post.Title,
                Description = post.ShortDescription,
                CreatedDate = post.CreatedDate.ToShortDateString()
            };
        }
    }
}
