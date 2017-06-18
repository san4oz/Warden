using System.Linq;
using System.Threading.Tasks;
using Warden.Business.Providers;
using Warden.Business.Utils;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Import.Processor.Steps
{
    public class TransactionRetreivingStep : ITransactionImportStep
    {        
        private IAPITransactionProvider transactionSource;

        public TransactionRetreivingStep(IAPITransactionProvider sourceProvider)
        {
            this.transactionSource = sourceProvider;
        }

        public void Execute(TransactionImportContext context)
        {
            var transactionRetreivingTask = Task.Run(async () =>
            {
                var transactions = await transactionSource.GetAsync(new TransactionRequest()
                {
                    PayerId = context.Request.PayerId,
                    From = context.Request.StartDate,
                    To = context.Request.EndDate,
                    OffsetNumber = context.Request.OffsetNumber
                });

                TransactionImportTracer.Trace(context.Request.PayerId, $"Parsed transaction count: {transactions.Count()}");

                context.Transactions = transactions.ToList();
            });

            Task.WaitAll(transactionRetreivingTask);
        }
    }
}
