using Warden.Business.Managers;
using Warden.Business.Providers;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionCreatingStep : ITransactionImportStep
    {
        private TransactionManager transactionManager;

        public TransactionCreatingStep(TransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }        
        public void Execute(TransactionImportContext context)
        {
            foreach(var item in context.Transactions)
            {
                transactionManager.Save(item);
            }
        }
    }
}
