using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionCreatingStep : ITransactionImportPipelineStep
    {
        private ITransactionDataProvider provider;

        public TransactionCreatingStep(ITransactionDataProvider provider)
        {
            this.provider = provider;
        }        
        public void Execute(TransactionImportPipelineContext context)
        {
            foreach(var item in context.Items)
            {
                provider.Save(item);
            }
        }
    }
}
