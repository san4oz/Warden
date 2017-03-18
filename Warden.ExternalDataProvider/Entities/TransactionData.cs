using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.ExternalDataProvider.Attributes;

namespace Warden.ExternalDataProvider.Entities
{
    public class TransactionData
    {
        [FieldIndex(6)]
        public string ReceiverId { get; set; }

        [FieldIndex(2)]
        public string PayerId { get; set; }

        [FieldIndex(10)]
        public decimal Price { get; set; }

        [FieldIndex(11)]
        public string Keywords { get; set; }

        [FieldIndex(1)]
        public DateTime Date { get; set; }
    }
}
