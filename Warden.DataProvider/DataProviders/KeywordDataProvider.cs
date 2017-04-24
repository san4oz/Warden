using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class KeywordDataProvider : BaseDataProvider<CategoryKeyword>, IKeywordProvider
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

        public List<CategoryKeyword> Get(string text)
        {
            return Execute(session =>
            {
                return session.QueryOver<CategoryKeyword>()
                            .Where(keyword => keyword.Keyword == text)
                            .List().ToList();
            });
        }
    }
}
