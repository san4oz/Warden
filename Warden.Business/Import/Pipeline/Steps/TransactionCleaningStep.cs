using System.Linq;
using Warden.Business.Managers;
using Warden.Business.Providers;

namespace Warden.Business.Import.Pipeline.Steps
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
