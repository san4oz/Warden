using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        public BlogController(PostManager postManager)
        {
            this.postManager = postManager;
        }

        [HttpPost]
        public ActionResult Posts()
        {
            var posts = postManager.All();

            var models = posts.Select(p => CreatePostPreview(p)).ToList();
            var result = Json(models, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        [HttpPost]
        public ActionResult Post(Guid? postId)
        {
            if (!postId.HasValue)
                return Json(false);

            var post = postManager.GetWithComponents(postId.Value);
            var model = CreatePostDetailsModel(post);

            var result = Json(model);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        private PostPreviewViewModel CreatePostPreview(Post post) => new PostPreviewViewModel()
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.ShortDescription,
            CreatedDate = post.CreatedDate.ToShortDateString(),
            Banner = post.Banner
        };

        private PostDetailsViewModel CreatePostDetailsModel(Post post)
        {
            return new PostDetailsViewModel()
            {
                Title = post.Title,
                Description = post.ShortDescription,
                CreatedDate = post.CreatedDate.ToShortDateString(),
                Banner = post.Banner,
                Components = CreatePostComponents(post.Components)
            };
        }

        private IEnumerable<PostComponentViewModel> CreatePostComponents(IEnumerable<PostComponent> components)
        {
            return components.Select(x => CreatePostComponent(x)).ToList();
        }

        private PostComponentViewModel CreatePostComponent(PostComponent component)
        {
            return new PostComponentViewModel()
            {
                Data = component.Data,
                PostId = component.PostId
            };
        }
    }
}
