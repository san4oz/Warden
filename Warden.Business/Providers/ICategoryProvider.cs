using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ICategoryProvider : IProvider<Category>
    {
        Category GetByTitle(string title);

        List<Category> GetByIds(Guid[] ids);
    }
}
