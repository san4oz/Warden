using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface IKeywordDataProvider : IDataProvider<CategoryKeyword>
    {
        CategoryKeyword Get(string text, Guid categoryId);
    }
}
