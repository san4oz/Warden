using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Contracts.Pipeline
{
    public class TransactionImportPipelineContext : IPipelineContext
    {
        public List<Transaction> Items { get; set; }

        public TransactionRequest Request { get; set; }
    }
}
