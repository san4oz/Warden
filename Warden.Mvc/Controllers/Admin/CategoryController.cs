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
        public ActionResult UnprocessedKeywords()
        {
            var keywords = categoryProvider.GetUprocessedKeywords();
            return Json(keywords);
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
        public ActionResult AttachKeywordToCategory(Guid keywordId, Guid categoryId)
        {
            var keyword = categoryProvider.GetUprocessedKeyword(keywordId);
            if (keyword == null)
                return HttpNotFound();

            var category = categoryProvider.Get(categoryId);
            if (category == null)
                return HttpNotFound();

            category.Keywords = string.Format("{0};{1}", category.Keywords, keyword.Value).Trim(new[] { ';' });
            categoryProvider.Save(category);
            categoryProvider.DeleteUnprocessedKeyword(keywordId);

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
