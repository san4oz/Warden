using System.Linq;
using Warden.Business.Managers;
using Warden.Business.Providers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionCleaningStep : ITransactionImportPipelineStep
    {
        private ITransactionProvider provider;
        private ISearchManager search;

        public void Execute(TransactionImportPipelineContext context)
        {
            var transactionsIds = provider.GetByPayerId(context.Request.PayerId).Select(p => p.Id).ToArray();
            provider.Delete(transactionsIds);
            search.CleanIndexEntries(transactionsIds);
        }

        public TransactionCleaningStep(ITransactionProvider provider, ISearchManager searchManager)
        {
            this.provider = provider;
            this.search = searchManager;
        }
    }
}
