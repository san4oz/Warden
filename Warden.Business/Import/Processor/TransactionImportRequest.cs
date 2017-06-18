using System;

namespace Warden.Business.Import.Processor
{
    public class TransactionImportRequest
    {
        public string PayerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int OffsetNumber { get; set; }

        public bool Rebuild { get; set; }
    }
}
