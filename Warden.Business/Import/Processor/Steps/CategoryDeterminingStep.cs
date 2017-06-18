using Warden.Business.Utils;
using Warden.Business.Managers;

namespace Warden.Business.Import.Processor.Steps
{
    public class CategoryDeterminingStep : ITransactionImportStep
    {
        private readonly AnalysisManager analysisManager;

        public CategoryDeterminingStep(AnalysisManager analysisManager)
        {
            this.analysisManager = analysisManager;
        }

        public void Execute(TransactionImportContext context)
        {
            context.Transactions.ForEach(transaction =>
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
