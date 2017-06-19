using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models.Frontend
{
    public class PayerDetailsViewModel
    {
        public string Name { get; set; }

        public decimal Total { get; set; }

        public string HighestSpendingsCategory { get; set; }

        public string LowestSpendingsCategory { get; set; }

        public int TransactionsCount { get; set; }

        public ChartData Data { get; set; }
    }
}
