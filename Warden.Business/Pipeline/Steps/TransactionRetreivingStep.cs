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
    public class TransactionRetreivingStep : IPipelineStep
    {        
        private IExternalApi api;

        private TransactionImportPipelineContext context;

        public TransactionRetreivingStep(IExternalApi api)
        {
            this.api = api;
        }

        public void Execute(IPipelineContext context)
        {
            this.context = (TransactionImportPipelineContext)context;

            var transactionRetreivingTask = Task.Run(async () =>
            {
                var transactions = await RetreiveTransaction(this.context.Request);
                this.context.Items = transactions.ToList();
            });

            Task.WaitAll(transactionRetreivingTask);
        }

        public async Task<IList<Transaction>> RetreiveTransaction(TransactionRequest request)
        {
            return await api.GetTransactionsAsync(request);
        }
    }
}
