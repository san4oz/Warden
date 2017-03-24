using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionCreatingStep : IPipelineStep
    {
        private TransactionImportPipelineContext context;
        private ITransactionDataProvider provider;

        public TransactionCreatingStep(ITransactionDataProvider provider)
        {
            this.provider = provider;
        }        
        public void Execute(IPipelineContext context)
        {
            this.context = (TransactionImportPipelineContext)context;

            foreach(var item in this.context.Items)
            {
                provider.Save(item);
            }
        }
    }
}
