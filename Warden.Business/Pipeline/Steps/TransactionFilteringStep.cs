using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Core;

namespace Warden.Business.Pipeline.Steps
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
