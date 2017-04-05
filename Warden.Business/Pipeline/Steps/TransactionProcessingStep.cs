using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionProcessingStep : ITransactionImportPipelineStep
    {
        public void Execute(TransactionImportPipelineContext context)
        {
            var normalizer = new TextNormalizer();
            foreach(var item in context.Items)
            {
                item.Keywords = normalizer.Normalize(item.Keywords);
            }
        }
    }
}
