using System;
using Warden.Business.Entities;
using Warden.Core.TextParsers;

namespace Warden.TransactionSource
{
    public class TransactionSourceItem
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

        [FieldIndex(0)]
        public string ExternalId { get; set; }

        public Transaction ToWardenTransaction()
        {
            return new Transaction()
            {
                PayerId = PayerId,
                ReceiverId = ReceiverId,
                Price = Price,
                Date = Date,
                Keywords = Keywords,
                ExternalId = ExternalId
            };
        }
    }
}
