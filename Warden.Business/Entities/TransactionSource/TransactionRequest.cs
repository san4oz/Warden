using System;

namespace Warden.Business.Entities.ExternalProvider
{
    public class TransactionRequest
    {
        public string PayerId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int OffsetNumber { get; set; }
    }
}
