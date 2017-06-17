using System.Collections.Generic;
using Warden.Business.Import.Pipeline.Steps;

namespace Warden.Business.Import.Pipeline
{
    public class TransactionImportPipeline
    {
        public IList<ITransactionImportPipelineStep> Steps { get; set; }

        public void Execute(TransactionImportRequest request)
        {
            RegisterSteps();

            var context = new TransactionImportPipelineContext() { Request = request };

            foreach (var step in this.Steps)
            {
                step.Execute(context);
            }
        }

        protected void RegisterSteps()
        {
            this.Steps = new List<ITransactionImportPipelineStep>()
            {
                new TransactionRetreivingStep(),
                new TransactionFilteringStep(),
                new TransactionProcessingStep(),
                new TransactionGroupingStep(),
                new TransactionCreatingStep(),
                new TransactionIndexingStep()
            };
        }
    }
}
