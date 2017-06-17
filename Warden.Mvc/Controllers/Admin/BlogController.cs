using System;
using System.Web.Mvc;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Mvc.Models.Admin;
using System.Linq;

namespace Warden.Mvc.Controllers.Admin
{
    public class BlogController : Controller
    {
        private readonly PostManager postManager;

        public BlogController(PostManager postManager)
        {
            this.postManager = postManager;
        }

        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            var postEntity = CreatePost(post);
            var postComponents = post.Components.Select(c => CreatePostComponent(postEntity.Id, c));

            postManager.SaveWithComponents(postEntity, postComponents);

            return Json(true);
        }

        private Post CreatePost(PostViewModel postModel)
        {
            return new Post()
            {
                Title = postModel.Title,
                Banner = postModel.Banner,
                ShortDescription = postModel.Description,
                CreatedDate = DateTime.Now
            };
        }

        private PostComponent CreatePostComponent(Guid postId, PostComponentViewModel postComponentModel)
        {           
            if(Enum.TryParse<ComponentType>(postComponentModel.Type, out ComponentType type))
            {
                return new PostComponent()
                {
                    PostId = postId,
                    Data = postComponentModel.Html,
                    Type = type
                };
            }

            return null;
        }
    }
}
