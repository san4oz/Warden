using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Scheduler
{
    public class TransactionExtractionTask : ITransactionExtractionTask
    {
        private ITransactionTaskConfigurationDataProvider configurationDataProvider;
        private TransactionExtractionTaskConfiguration configuration;
        private IExternalApi externalApi;
        private ITransactionDataProvider dataProvider;
        private IPayerDataProvider payerDataProvider;

        public TransactionExtractionTask
        (
            IExternalApi api,
            ITransactionDataProvider dataProvider,
            ITransactionTaskConfigurationDataProvider configurationDataProvider,
            IPayerDataProvider payerDataProvider
        )
        {
            this.externalApi = api;
            this.dataProvider = dataProvider;
            this.configurationDataProvider = configurationDataProvider;
            this.payerDataProvider = payerDataProvider;
        }



        void ITask.Execute()
        {
            if (!string.IsNullOrEmpty(this.configuration.PayerId))
                ExecuteExect(this.configuration.PayerId);
            else
                ExecuteAll();                    
        }
       
        public void RunExect(string payerId)
        {
           this.configuration =  this.configurationDataProvider.GetForPayer(payerId);
            ((ITask)this).Execute();
        }

        public void RunAll()
        {
            this.configuration = this.configurationDataProvider.GetDefault(null);
            ((ITask)this).Execute();
        }

        protected void ExecuteExect(string payerId)
        {
            var request = new TransactionRequest()
            {
                From = this.configuration.StartDate,
                To = this.configuration.EndDate,
                PayerId = this.configuration.PayerId
            };

            var tokenizer = new TextNormalizer();

            Task.Run(async () =>
            {
                var transactions = await externalApi.GetTransactionsAsync(request);
                foreach (var transaction in transactions)
                {
                    transaction.Keywords = string.Join(";", tokenizer.Tokenize(transaction.Keywords));
                    dataProvider.Save(transaction);
                }
            });
        }

        protected void ExecuteAll()
        {
            payerDataProvider.All().ForEach(payer => ExecuteExect(payer.PayerId));
        }
    }
}
