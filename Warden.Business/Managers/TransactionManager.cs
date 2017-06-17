﻿using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;
using Warden.Business.Providers;
using Warden.Core.Extensions;

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
            if (keyword.IsEmpty())
                return Provider.GetWithoutCategory().ToList();

            var request = new SearchRequest() { Query = keyword, IsWildCardSearch = true };
            var response = searchManager.Search(request);
            return Provider.GetWithoutCategory(response.Results.Select(i => new Guid(i.Id)).ToArray());
        }

        public void MarkAsVoted(Guid transactionId) => Provider.MarkAsVoted(transactionId);

        public void AttachToCategory(List<Guid> ids, Guid categoryId) => Provider.AttachToCategory(ids, categoryId);

        public List<Transaction> GetNotVoted(Guid categoryId) => Provider.GetNotVoted(categoryId);

        public List<Transaction> GetByCategoryId(Guid categoryId) => Provider.GetByCategoryId(categoryId);

        public int GetTotalCount() => Provider.GetTotalCount();

        public int GetCountByPayerId(string payerId) => Provider.GetCountByPayerId(payerId);

        public void DeleteByPayerId(string payerId) => Provider.DeleteByPayerId(payerId);

        public List<Transaction> GetTransactionsByPayerId(string payerId) => Provider.GetByPayerId(payerId);

        public List<Transaction> GetByGroupId(string groupId) => Provider.GetByGroupId(groupId);
    }
}
