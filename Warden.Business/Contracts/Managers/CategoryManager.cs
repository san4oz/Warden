using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Managers
{
    public class CategoryManager : EntityManager<Category, ICategoryDataProvider>
    {
        public CategoryManager(ICategoryDataProvider provider) : base(provider)
        {
        }

        public bool DoesCategoryExist(string title) => Provider.GetByTitle(title) != null;
    }
}
