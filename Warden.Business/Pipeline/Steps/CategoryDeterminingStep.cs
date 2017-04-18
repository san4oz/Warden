using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Managers;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Core;

namespace Warden.Business.Pipeline.Steps
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
