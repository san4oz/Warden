using System;

namespace Warden.Mvc.Models.Admin
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public string PayerId { get; set; }

        public decimal Price { get; set; }

        public string[] Keywords { get; set; }

        public bool HasSameTransactions { get; set; }

        public TransactionViewModel(Guid id, string payerId, decimal price, string keywords, bool hasSameTransactions)
        {
            this.Id = id;
            this.PayerId = payerId;
            this.Price = price;
            this.Keywords = keywords.Split(new[] { ';' });
            this.HasSameTransactions = hasSameTransactions;
        }
    }
}
