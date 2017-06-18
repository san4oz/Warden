using System.Linq;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Mvc.Models.Admin;

namespace Warden.Mvc.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private readonly CategoryManager categoryManager;

        public CategoryController(CategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            if (categoryManager.DoesCategoryExist(model.Title))
                return Json(new { error = $"Item with title '{model.Title}' already exists" });

            var category = new Category()
            {
                Title = model.Title,
                Keywords = model.Keywords
            };

            categoryManager.Save(category);

            return Json(true);
        }

        [HttpPost]
        public ActionResult All()
        {
            var categories = categoryManager.All()
                                .Select(c => new CategoryViewModel() { Id = c.Id, Title = c.Title, Keywords = c.Keywords }).OrderBy(x => x.Title);

            return Json(categories);
        }
    }
}
