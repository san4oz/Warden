using Warden.Business.Utils.Text;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionProcessingStep : ITransactionImportStep
    {
        public void Execute(TransactionImportContext context)
        {
            var normalizer = new TextNormalizer();
            foreach(var item in context.Transactions)
            {
                item.Keywords = normalizer.Normalize(item.Keywords);
            }
        }
    }
}
