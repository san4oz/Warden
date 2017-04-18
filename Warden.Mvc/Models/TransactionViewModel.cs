using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public string PayerId { get; set; }

        public decimal Price { get; set; }

        public string[] Keywords { get; set; }

        public TransactionViewModel(Guid id, string payerId, decimal price, string keywords)
        {
            this.Id = id;
            this.PayerId = payerId;
            this.Price = price;
            this.Keywords = keywords.Split(new[] { ';' });
        }
    }
}
