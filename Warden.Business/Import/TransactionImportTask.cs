using System;
using System.Collections.Concurrent;
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
        private readonly ITransactionImportConfigurationDataProvider configurationDataProvider;
        private readonly IPayerDataProvider payerDataProvider;
        private readonly ITransactionImportPipeline pipeline;
        private readonly ITransactionDataProvider transactionProvider;
        private bool initialized;
        private static ConcurrentDictionary<string, TransactionImportTaskConfiguration> Configurations { get; set; }

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

            Configurations = new ConcurrentDictionary<string, TransactionImportTaskConfiguration>();
        }

        public ImportTaskStatus StartImport(string payerId = null)
        {
            if (!initialized)
                throw new InvalidOperationException("Transaction import task wasn't initialized");

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

            return ImportTaskStatus.Finished;
        }

        protected void StartImportForPayer(string payerId)
        {
            if (string.IsNullOrEmpty(payerId))
                return;

            UpdateTaskStatus(payerId, ImportTaskStatus.InProgress);

            try
            { 
                while (true)
                {
                    var request = BuildImportRequest(payerId);

                    pipeline.Execute(request);

                    if(ShouldTryToImportMore(payerId))
                    {
                        UpdateItemsCount(payerId);
                        
                    }
                    else
                    {
                        UpdateTaskStatus(payerId, ImportTaskStatus.Finished);
                        break;
                    }
                }
            }
            catch
            {
                UpdateTaskStatus(payerId, ImportTaskStatus.Failed);
            }
        }

        public void Initialize()
        {
            foreach(var payer in payerDataProvider.All())
            {
                InitializeTaskForPayer(payer.PayerId);
            }

            OnAfterTaskInitialization();

            this.initialized = true;
        }

        protected void InitializeTaskForPayer(string payerId)
        {            
            if(!Configurations.ContainsKey(payerId))
            {
                var configuration = configurationDataProvider.GetForPayer(payerId);

                Configurations.TryAdd(payerId, configuration);               
            }
        }

        protected void OnAfterTaskInitialization()
        {
            foreach (var config in Configurations)
            {
                if(config.Value.Status == ImportTaskStatus.InProgress)
                {
                    config.Value.Status = ImportTaskStatus.Failed;
                    configurationDataProvider.Save(config.Value);
                }
            }
        }

        protected bool ShouldTryToImportMore(string payerId)
        {
            if(Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                return config.TransactionCount < transactionProvider.GetTransactionCountForPayer(payerId);
            }
            else
            {
                return false;
            }
        }

        protected void UpdateTaskStatus(string payerId, ImportTaskStatus status)
        {
            if(Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                config.Status = status;
                configurationDataProvider.Save(config);
            }
        }

        protected void UpdateItemsCount(string payerId)
        {
            if(Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                config.TransactionCount = transactionProvider.GetTransactionCountForPayer(payerId);
            }
        }

        protected TransactionImportRequest BuildImportRequest(string payerId)
        {
            if(Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                return new TransactionImportRequest
                {
                    FromDate = config.StartDate,
                    PayerId = config.PayerId,
                    ToDate = config.EndDate,
                    OffsetNumber = config.TransactionCount
                };
            }
            else
            {
                throw new InvalidOperationException($"Import task for payer {payerId} hasn't been initialized");
            }
        }
    }
}
