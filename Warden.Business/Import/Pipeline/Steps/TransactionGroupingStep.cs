using System;
using System.Linq;
using Warden.Business.Helpers;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionGroupingStep : ITransactionImportPipelineStep
    {
        public void Execute(TransactionImportPipelineContext context)
        {
            var groupedTransactions = context.Items.GroupBy(item => item.Keywords);

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
