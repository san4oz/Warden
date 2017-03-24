using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionIndexingStep : IPipelineStep
    {
        private TransactionImportPipelineContext context;

        private ISearchManager search;

        public void Execute(IPipelineContext context)
        {
            this.context = (TransactionImportPipelineContext)context;

            search.Index(this.context.Items);
        }

        public TransactionIndexingStep(ISearchManager search)
        {
            this.search = search;
        }
    }
}
