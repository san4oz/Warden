using Warden.Business.Managers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionIndexingStep : ITransactionImportPipelineStep
    {
        private ISearchManager search;

        public void Execute(TransactionImportPipelineContext context)
        {
            search.Index(context.Items, context.Request.Rebuild);
        }

        public TransactionIndexingStep()
        {
            this.search = IoC.Resolve<ISearchManager>();
        }
    }
}
