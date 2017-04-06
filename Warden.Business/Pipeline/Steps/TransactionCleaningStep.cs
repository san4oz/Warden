using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionCleaningStep : ITransactionImportPipelineStep
    {
        private ITransactionDataProvider provider;
        private ISearchManager search;

        public void Execute(TransactionImportPipelineContext context)
        {
            var transactionsIds = provider.GetTransactionsByPayerId(context.Request.PayerId).Select(p => p.Id).ToArray();
            provider.Delete(transactionsIds);
            search.CleanIndexEntries(transactionsIds);
        }

        public TransactionCleaningStep(ITransactionDataProvider provider, ISearchManager searchManager)
        {
            this.provider = provider;
            this.search = searchManager;
        }
    }
}
