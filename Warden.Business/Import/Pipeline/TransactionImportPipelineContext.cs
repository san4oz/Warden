using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Import.Pipeline
{
    public class TransactionImportPipelineContext
    {
        public List<Transaction> Items { get; set; }

        public TransactionImportRequest Request { get; set; }
    }
}
