using System.Collections.Generic;
using Warden.Business.Import.Processor.Steps;
using Warden.Business.Providers;

namespace Warden.Business.Import.Processor
{
    public class TransactionImportProcessor
    {
        private readonly IEnumerable<ITransactionImportStep> steps;

        public TransactionImportProcessor(IEnumerable<ITransactionImportStep> steps)
        {
            this.steps = steps;
        }

        public void Execute(TransactionImportRequest request)
        {
            var context = new TransactionImportContext() { Request = request };

            foreach (var step in this.steps)
            {
                step.Execute(context);
            }
        }       
    }
}
