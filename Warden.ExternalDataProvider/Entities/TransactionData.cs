using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Core.TextParsers;

namespace Warden.ExternalDataProvider.Entities
{
    public class TransactionData
    {
        [FieldIndex(7)]
        public string ReceiverId { get; set; }

        [FieldIndex(3)]
        public string PayerId { get; set; }

        [FieldIndex(12)]
        public decimal Price { get; set; }

        [FieldIndex(11)]
        public string Keywords { get; set; }

        [FieldIndex(2)]
        public DateTime Date { get; set; }
    }
}
