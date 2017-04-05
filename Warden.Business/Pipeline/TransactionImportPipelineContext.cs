using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Business.Pipeline;

namespace Warden.Business.Contracts.Pipeline
{
    public class TransactionImportPipelineContext
    {
        public List<Transaction> Items { get; set; }

        public TransactionImportRequest Request { get; set; }
    }
}
