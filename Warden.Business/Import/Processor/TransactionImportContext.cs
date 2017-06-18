using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Import.Processor
{
    public class TransactionImportContext
    {
        public List<Transaction> Transactions { get; set; }

        public TransactionImportRequest Request { get; set; }
    }
}
