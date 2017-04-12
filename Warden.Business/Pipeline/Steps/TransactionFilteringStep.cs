using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionFilteringStep : ITransactionImportPipelineStep
    {
        public void Execute(TransactionImportPipelineContext context)
        {
            context.Items = context.Items.Where(i => i.PayerId == context.Request.PayerId).ToList();
        }
    }
}
