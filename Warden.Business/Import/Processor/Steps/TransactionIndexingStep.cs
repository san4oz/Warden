using Warden.Business.Managers;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionIndexingStep : ITransactionImportStep
    {
        private ISearchManager search;

        public void Execute(TransactionImportContext context)
        {
            search.Index(context.Transactions);
        }

        public TransactionIndexingStep(ISearchManager searchManager)
        {
            this.search = searchManager;
        }
    }
}
