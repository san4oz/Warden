using System;
using System.Collections.Generic;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class CategoryManager : EntityManager<Category, ICategoryProvider>
    {
        public CategoryManager(ICategoryProvider provider) : base(provider)
        {
        }

        public bool DoesCategoryExist(string title) => Provider.GetByTitle(title) != null;

        public List<Category> GetByIds(Guid[] ids) => Provider.GetByIds(ids);
    }
}
