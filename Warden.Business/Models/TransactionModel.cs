using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Models
{
    public class TransactionModel
    {
        public string Category { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
