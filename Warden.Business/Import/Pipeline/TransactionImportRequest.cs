using System;

namespace Warden.Business.Import.Pipeline
{
    public class TransactionImportRequest
    {
        public string PayerId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int OffsetNumber { get; set; }
    }
}
