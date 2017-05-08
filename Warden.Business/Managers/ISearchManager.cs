using System;
using System.Collections.Generic;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;

namespace Warden.Business.Managers
{
    public interface ISearchManager
    {
        void Index(IEnumerable<Transaction> transactions, bool rebuild);

        SearchResponse Search(SearchRequest request);
    }
}
