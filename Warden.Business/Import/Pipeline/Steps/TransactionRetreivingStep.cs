using System.Linq;
using System.Threading.Tasks;
using Warden.Business.Providers;
using Warden.Business.Helper;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Import.Pipeline.Steps
{
    public class TransactionRetreivingStep : ITransactionImportPipelineStep
    {        
        private ITransactionSourceProvider transactionSource;

        public TransactionRetreivingStep()
        {
            this.transactionSource = IoC.Resolve<ITransactionSourceProvider>();
        }

        public void Execute(TransactionImportPipelineContext context)
        {
            var transactionRetreivingTask = Task.Run(async () =>
            {
                var transactions = await transactionSource.GetAsync(new TransactionRequest()
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
