using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Api;
using Warden.Business.Contracts.Pipeline;
using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionProcessingStep : IPipelineStep
    {
        private TransactionImportPipelineContext context;

        private ITransactionTextProcessor processor;

        public TransactionProcessingStep(ITransactionTextProcessor processor)
        {
            this.processor = processor;
        }

        public void Execute(IPipelineContext context)
        {
            this.context = (TransactionImportPipelineContext)context;

            this.processor.MakeUpKeywords(this.context.Items);
        }
    }
}
