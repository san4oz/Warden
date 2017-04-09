using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Business.Pipeline;
using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Import
{
    public class TransactionImportTask : ITransactionImportTask
    {
        private ITransactionImportConfigurationDataProvider configurationDataProvider;
        private IPayerDataProvider payerDataProvider;
        private ITransactionImportPipeline pipeline;
        private ITransactionDataProvider transactionProvider;

        public TransactionImportTask
        (
            ITransactionImportConfigurationDataProvider configurationDataProvider,
            IPayerDataProvider payerDataProvider,
            ITransactionImportPipeline pipeline,
            ITransactionDataProvider transactionProvider
        )
        {
            this.configurationDataProvider = configurationDataProvider;
            this.payerDataProvider = payerDataProvider;
            this.pipeline = pipeline;
            this.transactionProvider = transactionProvider;
        }

        public void StartImport(string payerId = null)
        {
            if(!string.IsNullOrEmpty(payerId))
            {
                StartImportForPayer(payerId);
            }
            else
            {
                var payers = payerDataProvider.All();
                foreach (var payer in payers)
                {
                    StartImportForPayer(payer.PayerId);
                }
            }
        }

        protected void StartImportForPayer(string payerId)
        {
            if (string.IsNullOrEmpty(payerId))
                return;

            var configuration = configurationDataProvider.GetForPayer(payerId);
            while (true)
            {
                var currentTransactionCount = transactionProvider.GetTransactionCountForPayer(payerId);

                pipeline.Execute(new TransactionImportRequest()
                {
                    FromDate = configuration.StartDate,
                    PayerId = configuration.PayerId,
                    ToDate = configuration.EndDate,
                    OffsetNumber = currentTransactionCount
                });

                if(!ShouldTryToImportMore(configuration.PayerId, currentTransactionCount))
                    break;
            }
        }

        protected bool ShouldTryToImportMore(string payerId, int oldTransactionCount)
        {
            var currentTransactionCount = transactionProvider.GetTransactionCountForPayer(payerId);
            return oldTransactionCount < currentTransactionCount;
        }
    }
}
