using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;

namespace Warden.Business.Contracts.Providers
{
    public interface ISearchManager
    {
        void Index(IEnumerable<Transaction> transactions);

        SearchResponse Search(SearchRequest request);

        void CleanIndexEntries(Guid[] ids);
    }
}
