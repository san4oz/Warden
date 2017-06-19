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

            var model = new PayerModel();
            model.Name = payer.Name;

            return model;
        }

        public IList<PayerModel> GetAvailablePayers()
        {
            return payerManager.All().Select(p => new PayerModel() { Name = p.Name, Id = p.PayerId }).ToList();
        }

        public IList<TransactionModel> GetTransactions(string payerId)
        {
            var transactions = transactionManager.GetWithCategoryByPayerId(payerId);
            if (transactions.Count <= 0)
                return new List<TransactionModel>();

            return transactions.Select(t => CreateTransactionModel(t)).ToList();
        }

        public decimal CalculateTotal(IEnumerable<TransactionModel> transactions)
        {
            return transactions.Sum(t => t.TotalPrice);
        }

        public string GetHighestSpendingsCategory(IEnumerable<TransactionModel> transactions)
        {
            if (transactions.Count() <= 0)
                return null;

            return transactions.OrderByDescending(t => t.TotalPrice).First().Category;
        }

        public string GetLowestSpedningsCategory(IEnumerable<TransactionModel> transactions)
        {
            if (transactions.Count() <= 0)
                return null;

            return transactions.OrderBy(t => t.TotalPrice).First().Category;
        }

        public TransactionModel CreateTransactionModel(Transaction transaction)
        {
            if (!transaction.CategoryId.HasValue)
                return null;

            var category = categoryManager.GetByIds(new[] { transaction.CategoryId.Value }).FirstOrDefault();
            if (category == null)
                return null;

            return new TransactionModel() { Category = category.Title, TotalPrice = transaction.Price };
        }
    }
}
