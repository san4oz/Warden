using System.Linq;
using Warden.Business.Helpers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionFilteringStep : ITransactionImportPipelineStep
    {
        public void Execute(TransactionImportPipelineContext context)
        {
            var notValidTransaction = context.Items.Where(i => !i.PayerId.Equals(context.Request.PayerId)).ToList();

            if(notValidTransaction.Count() > 0)
            {
                TransactionImportTracer.Trace(context.Request.PayerId, $"There are {notValidTransaction.Count()} invalid transactions:");

                notValidTransaction.ForEach(transaction =>
                {
                    TransactionImportTracer.Trace(context.Request.PayerId, $"PayerId: {transaction.PayerId} | Price: {transaction.Price} | 007.org Id: {transaction.ExternalId}");
                });
            }

            context.Items.RemoveAll(t => notValidTransaction.Select(tr => tr.Id).Contains(t.Id));
        }
    }
}
