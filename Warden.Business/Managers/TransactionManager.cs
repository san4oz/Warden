using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class TransactionManager : EntityManager<Transaction, ITransactionProvider>
    {
        private readonly ISearchManager searchManager;

        public TransactionManager(ITransactionProvider transactionProvider, ISearchManager searchManager) : base(transactionProvider)
        {
            this.searchManager = searchManager;
        }

        public List<Transaction> GetWithoutCategory(string keyword)
        {
            var request = new SearchRequest() { Query = keyword, IsWildCardSearch = true };
            var response = searchManager.Search(request);
            return Provider.GetWithoutCategory(response.Results.Select(i => new Guid(i.Id)).ToArray());
        }

        public void MarkAsVoted(Guid transactionId) => Provider.MarkAsVoted(transactionId);

        public void AttachToCategory(Guid transactionId, Guid categoryId) => Provider.AttachToCategory(transactionId, categoryId);

        public List<Transaction> GetNotVoted(Guid categoryId) => Provider.GetNotVoted(categoryId);

        public List<Transaction> GetByCategoryId(Guid categoryId) => Provider.GetByCategoryId(categoryId);

        public int GetTotalCount() => Provider.GetTotalCount();

        public int GetCountByPayerId(string payerId) => Provider.GetCountByPayerId(payerId);

        public void DeleteByPayerId(string payerId) => Provider.DeleteByPayerId(payerId);
    }
}
