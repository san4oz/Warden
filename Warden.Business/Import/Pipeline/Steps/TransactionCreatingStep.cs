using Warden.Business.Providers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionCreatingStep : ITransactionImportPipelineStep
    {
        private ITransactionProvider provider;

        public TransactionCreatingStep()
        {
            this.provider = IoC.Resolve<ITransactionProvider>();
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
