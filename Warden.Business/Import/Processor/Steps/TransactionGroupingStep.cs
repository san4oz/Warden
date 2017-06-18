using System;
using System.Linq;
using Warden.Business.Utils;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionGroupingStep : ITransactionImportStep
    {
        public void Execute(TransactionImportContext context)
        {
            var groupedTransactions = context.Transactions.GroupBy(item => item.Keywords);

            foreach(var group in groupedTransactions)
            {
                if (group.Count() > 1)
                {
                    TransactionImportTracer.Trace(context.Request.PayerId, $"{group.Count()} transactions will be grouped by the following keywords {group.Key}");

                    var groupId = Guid.NewGuid().ToString();

                    foreach(var transaction in group)
                    {
                        transaction.GroupId = groupId;
                    }
                }
            }
        }
    }
}
