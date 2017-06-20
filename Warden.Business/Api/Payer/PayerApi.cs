using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Business.Models;
using Warden.Core.Extensions;

namespace Warden.Business.Api.Payer
{
    public class PayerApi
    {
        private readonly TransactionManager transactionManager;
        private readonly CategoryManager categoryManager;
        private readonly PayerManager payerManager;

        public PayerApi(TransactionManager transactionManager, CategoryManager categoryManager, PayerManager payerManager)
        {
            this.transactionManager = transactionManager;
            this.categoryManager = categoryManager;
            this.payerManager = payerManager;
        }

        public PayerModel GetPayer(string payerId)
        {
            if (payerId.IsEmpty())
                return null;

            var payer = payerManager.Get(payerId);
            if (payer == null)
                return null;

            return new PayerModel(payer);
        }

        public IList<PayerModel> GetAvailablePayers()
        {
            return payerManager.All().Select(p => new PayerModel(p)).ToList();
        }

        public IList<Spending> GetSpendings(string payerId)
        {
            var transactions = transactionManager.GetWithCategoryByPayerId(payerId);
            if (transactions.Count <= 0)
                return new List<Spending>();

            var groupedTransactions = transactions.GroupBy(t => t.CategoryId)
                                                  .Select(group => new Transaction()
                                                  {
                                                      Price = group.Sum(t => t.Price),
                                                      CategoryId = group.Key
                                                  });

            return groupedTransactions.Select(t => CreateSpendingModel(t)).ToList();
        }

        public decimal CalculateTotal(IEnumerable<Spending> transactions)
        {
            return transactions.Sum(t => t.TotalPrice);
        }

        public string GetHighestSpendingCategory(IEnumerable<Spending> transactions)
        {
            if (transactions.Count() <= 0)
                return null;

            return transactions.OrderByDescending(t => t.TotalPrice).First().Category;
        }

        public string GetLowestSpedningCategory(IEnumerable<Spending> transactions)
        {
            if (transactions.Count() <= 0)
                return null;

            return transactions.OrderBy(t => t.TotalPrice).First().Category;
        }

        public Spending CreateSpendingModel(Transaction transaction)
        {
            if (!transaction.CategoryId.HasValue)
                return null;

            var category = categoryManager.GetByIds(new[] { transaction.CategoryId.Value }).FirstOrDefault();
            if (category == null)
                return null;

            return new Spending() { Category = category.Title, TotalPrice = transaction.Price };
        }

        public int GetRegisteredTransactionsCount(string payerId)
        {
            return transactionManager.GetCountByPayerId(payerId);
        }
    }
}
