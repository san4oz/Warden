using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models.Frontend
{
    public class PayerStatisticViewModel
    {
        public decimal Total { get; set; }

        public string HighestSpendingsCategory { get; set; }

        public string LowestSpendingsCategory { get; set; }

        public int TransactionsCount { get; set; }

        public ChartData Data { get; set; }

        public bool IsEmpty
        {
            get { return Total <= 0; }
        }
    }
}
