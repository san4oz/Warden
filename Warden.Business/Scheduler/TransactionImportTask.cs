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

namespace Warden.Business.Scheduler
{
    public class TransactionImportTask : ITransactionImportTask
    {
        private TransactionImportTaskConfiguration configuration;

        private ITransactionTaskConfigurationDataProvider configurationDataProvider;
        private IPayerDataProvider payerDataProvider;
        private ITransactionImportPipeline pipeline;

        public TransactionImportTask
        (
            ITransactionTaskConfigurationDataProvider configurationDataProvider,
            IPayerDataProvider payerDataProvider,
            ITransactionImportPipeline pipeline
        )
        {
            this.configurationDataProvider = configurationDataProvider;
            this.payerDataProvider = payerDataProvider;
            this.pipeline = pipeline;
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
            pipeline.Execute(new TransactionImportRequest()
            {
                FromDate = this.configuration.StartDate,
                PayerId = this.configuration.PayerId,
                ToDate = this.configuration.EndDate
            });
        }

        protected void ExecuteAll()
        {
            payerDataProvider.All().ForEach(payer => ExecuteExect(payer.PayerId));
        }
    }
}
