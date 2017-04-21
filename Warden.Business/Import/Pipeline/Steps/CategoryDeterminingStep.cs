using Warden.Business.Helper;
using Warden.Business.Managers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class CategoryDeterminingStep : ITransactionImportPipelineStep
    {
        private readonly AnalysisManager analysisManager;

        public CategoryDeterminingStep(AnalysisManager analysisManager)
        {
            this.analysisManager = analysisManager;
        }

        public void Execute(TransactionImportPipelineContext context)
        {
            context.Items.ForEach(transaction =>
            {
                var result = analysisManager.TryAttachToCategory(transaction);
                if (result)
                {
                    TransactionImportTracer.Trace(context.Request.PayerId, $"Transaction with id {transaction.Id} will be automaticaly assigned to category");
                }
            });
        }
    }
}
