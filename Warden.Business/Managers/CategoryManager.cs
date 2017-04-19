using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class CategoryManager : EntityManager<Category, ICategoryDataProvider>
    {
        public CategoryManager(ICategoryDataProvider provider) : base(provider)
        {
        }

        public bool DoesCategoryExist(string title) => Provider.GetByTitle(title) != null;
    }
}
