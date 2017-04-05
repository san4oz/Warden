using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Mvc.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private ICategoryDataProvider categoryProvider;

        public CategoryController(ICategoryDataProvider categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return Json(false);

            if (categoryProvider.GetByTitle(category.Title) != null)
                return Json(new { error = $"Item with title '{category.Title}' already exists" });

            categoryProvider.Save(category);

            return Json(true);
        }

        [HttpPost]
        public ActionResult All()
        {
            var categories = categoryProvider.All().OrderBy(x => x.Title);
            return Json(categories);
        }
    }
}
