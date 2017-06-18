using System.Linq;
using Warden.Business.Managers;
using Warden.Business.Providers;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionCleaningStep : ITransactionImportStep
    {
        private ITransactionProvider provider;
        private ISearchManager search;

        public TransactionCleaningStep(ITransactionProvider provider, ISearchManager searchManager)
        {
            this.provider = provider;
            this.search = searchManager;
        }

        public void Execute(TransactionImportContext context)
        {
            var transactionsIds = provider.GetByPayerId(context.Request.PayerId).Select(p => p.Id).ToArray();
            provider.Delete(transactionsIds);
            search.CleanIndexEntries(transactionsIds);
        }


    }
}
