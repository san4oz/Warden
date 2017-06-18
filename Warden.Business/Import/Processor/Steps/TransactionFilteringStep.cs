using System.Linq;
using Warden.Business.Utils;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionFilteringStep : ITransactionImportStep
    {
        public void Execute(TransactionImportContext context)
        {
            var notValidTransaction = context.Transactions.Where(i => !i.PayerId.Equals(context.Request.PayerId)).ToList();

            if(notValidTransaction.Count() > 0)
            {
                TransactionImportTracer.Trace(context.Request.PayerId, $"There are {notValidTransaction.Count()} invalid transactions:");

                notValidTransaction.ForEach(transaction =>
                {
                    TransactionImportTracer.Trace(context.Request.PayerId, $"PayerId: {transaction.PayerId} | Price: {transaction.Price} | 007.org Id: {transaction.ExternalId}");
                });
            }

            context.Transactions.RemoveAll(t => notValidTransaction.Select(tr => tr.Id).Contains(t.Id));
        }
    }
}
