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

        [HttpPost]
        public ActionResult Post(Guid? postId)
        {
            if (!postId.HasValue)
                return Json(false);

            var post = postManager.Get(postId.Value);
            var model = CreatePostDetailsModel(post);

            return Json(model);
        }

        private PostPreviewViewModel CreatePostPreview(Post post)
        {
            return new PostPreviewViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.ShortDescription,
                CreatedDate = post.CreatedDate.ToShortDateString()
            };
        }

        private PostDetailsViewModel CreatePostDetailsModel(Post post)
        {
            return new PostDetailsViewModel()
            {
                Title = post.Title,
                Description = post.ShortDescription,
                CreatedDate = post.CreatedDate.ToShortDateString()
            };
        }
    }
}
