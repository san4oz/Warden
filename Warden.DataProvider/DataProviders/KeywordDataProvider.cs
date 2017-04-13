using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.DataProvider.DataProviders
{
    public class KeywordDataProvider : BaseDataProvider<CategoryKeyword>, IKeywordDataProvider
    {
        public CategoryKeyword Get(string text, Guid categoryId)
        {
            return Execute(session =>
            {
                return session.QueryOver<CategoryKeyword>()
                            .Where(keyword => keyword.Keyword == text)
                            .And(t => t.CategoryId == categoryId)
                            .SingleOrDefault();
            });
        }
    }
}
