using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Import.Pipeline.Steps
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
