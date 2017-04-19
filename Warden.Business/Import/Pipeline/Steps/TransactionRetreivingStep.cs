using System.Linq;
using System.Threading.Tasks;
using Warden.Business.Api;
using Warden.Business.Core;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionRetreivingStep : ITransactionImportPipelineStep
    {        
        private IExternalApi api;

        public TransactionRetreivingStep()
        {
            this.api = IoC.Resolve<IExternalApi>();
        }

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

                TransactionImportTracer.Trace(context.Request.PayerId, $"Parsed transaction count: {transactions.Count()}");

                context.Items = transactions.ToList();
            });

            Task.WaitAll(transactionRetreivingTask);
        }
    }
}
