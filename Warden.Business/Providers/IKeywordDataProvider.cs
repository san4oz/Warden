using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IKeywordDataProvider : IDataProvider<CategoryKeyword>
    {
        CategoryKeyword Get(string text, Guid categoryId);

        List<CategoryKeyword> Get(string text);
    }
}
