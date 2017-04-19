using System;

namespace Warden.Business.Entities.ExternalProvider
{
    public class TransactionRetreivingRequest
    {
        public string PayerId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int OffsetNumber { get; set; }
    }
}
