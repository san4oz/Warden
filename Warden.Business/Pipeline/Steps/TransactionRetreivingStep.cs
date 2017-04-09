using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Pipeline.Steps
{
    public class TransactionRetreivingStep : ITransactionImportPipelineStep
    {        
        private IExternalApi api;

        public TransactionRetreivingStep(IExternalApi api)
        {
            this.api = api;
        }

#warning this task will work sync...there is no reason to use it as it is
        public void Execute(TransactionImportPipelineContext context)
        {
            var transactionRetreivingTask = Task.Run(async () =>
            {
                var transactions = await api.GetTransactionsAsync(new TransactionRetreivingRequest()
                {
                    PayerId = context.Request.PayerId,
                    From = context.Request.FromDate,
                    To = context.Request.ToDate,
                    OffsetNumber = context.Request.OffsetNumber
                });
                context.Items = transactions.ToList();
            });

            Task.WaitAll(transactionRetreivingTask);
        }
    }
}
