using System;
using System.Collections.Generic;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;

namespace Warden.Business.Managers
{
    public interface ISearchManager
    {
        void Index(IEnumerable<Transaction> transactions);

        SearchResponse Search(SearchRequest request);

        void CleanIndexEntries(Guid[] ids);
    }
}
