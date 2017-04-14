using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;

namespace Warden.Business.Contracts.Managers
{
    public class TransactionManager : EntityManager<Transaction, ITransactionDataProvider>
    {
        private readonly ISearchManager searchManager;

        public TransactionManager(ITransactionDataProvider transactionProvider, ISearchManager searchManager) : base(transactionProvider)
        {
            this.searchManager = searchManager;
        }

        public List<Transaction> SearchForUnprocessed(string keyword)
        {
            var request = new SearchRequest() { Query = keyword, IsWildCardSearch = true };
            var response = searchManager.Search(request);
            var ids = response.Results.Select(i => new Guid(i.Id));

            return Provider.GetUnprocessedTransactions(ids.ToArray());
        }

        public List<Transaction> GetTransactionsToCalibrate(Guid categoryId) => Provider.GetTransactionsToCalibrate(categoryId);

        public List<Transaction> GetProcessedByCategoryId(Guid categoryId) => Provider.GetProcessedTransactions(categoryId);

        public int GetTotalCount() => Provider.GetGeneralTransactionCount();
    }
}
