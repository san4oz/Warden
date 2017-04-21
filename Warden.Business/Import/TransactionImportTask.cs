using System;
using System.Collections.Concurrent;
using Warden.Business.Helper;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Business.Import.Pipeline;
using Warden.Business.Providers;

namespace Warden.Business.Import
{
    public class TransactionImportTask
    {
        private readonly ITransactionImportConfigurationDataProvider configurationDataProvider;
        private readonly PayerManager payerManager;
        private readonly TransactionImportPipeline importPipeline;
        private readonly TransactionManager transactionManager;
        private bool initialized;
        private static ConcurrentDictionary<string, TransactionImportTaskConfiguration> Configurations { get; set; }

        public TransactionImportTask()
        {
            this.configurationDataProvider = IoC.Resolve<ITransactionImportConfigurationDataProvider>();
            this.payerManager = IoC.Resolve<PayerManager>();
            this.importPipeline = IoC.Resolve<TransactionImportPipeline>();
            this.transactionManager = IoC.Resolve<TransactionManager>();

            Configurations = new ConcurrentDictionary<string, TransactionImportTaskConfiguration>();
        }

        public ImportTaskStatus StartImport(string payerId = null, bool rebuild = false)
        {
            if (!initialized)
                throw new InvalidOperationException("Transaction import task wasn't initialized");

            if(!string.IsNullOrEmpty(payerId))
            {
                StartImportForPayer(payerId, rebuild);
            }
            else
            {
                var payers = payerManager.All();
                foreach (var payer in payers)
                {
                    StartImportForPayer(payer.PayerId, rebuild);
                }
            }

            return ImportTaskStatus.Finished;
        }

        protected void StartImportForPayer(string payerId, bool rebuild = false)
        {
            if (string.IsNullOrEmpty(payerId))
                return;
            if (rebuild)
            {
                TransactionImportTracer.Trace(payerId, "Rebuild taks was started.");
                transactionManager.DeleteByPayerId(payerId);
                UpdateItemsCount(payerId);
            }
            else
            {
                TransactionImportTracer.Trace(payerId, "Import taks was started.");
            }

            UpdateTaskStatus(payerId, ImportTaskStatus.InProgress);

            try
            {
                while (true)
                {
                    var request = BuildImportRequest(payerId);
                    importPipeline.Execute(request);

                    bool shouldContinue(string payer)
                    {
                        if (Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config) && ShouldTryToImportMore(payer, config))
                        {
                            UpdateItemsCount(payer, config);
                            return true;
                        }
                        UpdateTaskStatus(config, ImportTaskStatus.Finished);
                        return false;
                    };

                    if (!shouldContinue(payerId))
                        break;
                }

                TransactionImportTracer.Trace(payerId, $"Task has been successfully finished.");
            }
            catch(Exception ex)
            {
                TransactionImportTracer.Trace(payerId, $"Task was failed. Stack trace: {Environment.NewLine} {ex.StackTrace}");
                UpdateTaskStatus(payerId, ImportTaskStatus.Failed);
            }
        }

        public void Initialize()
        {
            foreach(var payer in payerManager.All())
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

        protected bool ShouldTryToImportMore(string payerId, TransactionImportTaskConfiguration config)
        {
            return config.TransactionCount < transactionManager.GetCountByPayerId(payerId);
        }

        protected void UpdateItemsCount(string payerId, TransactionImportTaskConfiguration config = null)
        {
            if (config != null || Configurations.TryGetValue(payerId, out config))
            {
                config.TransactionCount = transactionManager.GetCountByPayerId(payerId);
            }
        }

        protected void UpdateTaskStatus(TransactionImportTaskConfiguration config, ImportTaskStatus status)
        {
            if (config == null)
                return;
            config.Status = status;
            configurationDataProvider.Save(config);
        }

        protected void UpdateTaskStatus(string payerId, ImportTaskStatus status)
        {
            if (Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                TransactionImportTracer.Trace(payerId, $"Import task status will be changed from {config.Status} to {status.GetStringRepresentation()}");

                config.Status = status;
                configurationDataProvider.Save(config);
            }
        }

        protected TransactionImportRequest BuildImportRequest(string payerId)
        {
            if(Configurations.TryGetValue(payerId, out TransactionImportTaskConfiguration config))
            {
                TransactionImportTracer.Trace(payerId, $"Request: StartDate: {config.StartDate.ToShortDateString()}, ToDate: {config.EndDate.ToShortDateString()}, Offset: {config.TransactionCount }");

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
