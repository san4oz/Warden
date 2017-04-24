using System;
using System.Collections.Concurrent;
using Warden.Business.Helpers;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Business.Import.Pipeline;
using Warden.Business.Providers;

namespace Warden.Business.Import
{
    public class TransactionImportTask
    {
        private readonly ImportSettingsManager settingsManager;
        private readonly PayerManager payerManager;
        private readonly TransactionImportPipeline importPipeline;
        private readonly TransactionManager transactionManager;
        private bool initialized;
        private static ConcurrentDictionary<string, TransactionImportSettings> Settings { get; set; }

        public TransactionImportTask()
        {
            this.settingsManager = IoC.Resolve<ImportSettingsManager>();
            this.payerManager = IoC.Resolve<PayerManager>();
            this.importPipeline = IoC.Resolve<TransactionImportPipeline>();
            this.transactionManager = IoC.Resolve<TransactionManager>();

            Settings = new ConcurrentDictionary<string, TransactionImportSettings>();
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
#warning This function should not update task status or should be renamed.
            bool shouldContinue(string payer)
            {
                if (Settings.TryGetValue(payerId, out TransactionImportSettings settings) && ShouldTryToImportMore(payer, settings))
                {
                    UpdateItemsCount(payer, settings);
                    return true;
                }
                UpdateTaskStatus(payerId, ImportTaskStatus.Finished);
                return false;
            };

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
            this.initialized = true;

            OnAfterTaskInitialized();           
        }

        protected void InitializeTaskForPayer(string payerId)
        {            
            if(!Settings.ContainsKey(payerId))
            {
                var settings = settingsManager.GetByPayerId(payerId);

                Settings.TryAdd(payerId, settings);               
            }
        }

        protected void OnAfterTaskInitialized()
        {
            foreach (var settings in Settings)
            {
                if(settings.Value.Status == ImportTaskStatus.InProgress)
                {
                    UpdateTaskStatus(settings.Value.PayerId, ImportTaskStatus.Failed);
                }
            }
        }

        protected bool ShouldTryToImportMore(string payerId, TransactionImportSettings config)
        {
            return config.TransactionCount < transactionManager.GetCountByPayerId(payerId);
        }

        protected void UpdateItemsCount(string payerId, TransactionImportSettings settings = null)
        {
            if (settings != null || Settings.TryGetValue(payerId, out settings))
            {
                settings.TransactionCount = transactionManager.GetCountByPayerId(payerId);
            }
        }

        protected void UpdateTaskStatus(string payerId, ImportTaskStatus status)
        {
            if (Settings.TryGetValue(payerId, out TransactionImportSettings settings))
            {               
                TransactionImportTracer.Trace(payerId, $"Import task status will be changed from {settings.Status} to {status.GetStringRepresentation()}");
                settingsManager.UpdateTaskStatus(settings, status);
            }
        }

        protected TransactionImportRequest BuildImportRequest(string payerId)
        {
            if(Settings.TryGetValue(payerId, out TransactionImportSettings settings))
            {
                TransactionImportTracer.Trace(payerId, $"Request: StartDate: {settings.StartDate.ToShortDateString()}, ToDate: {settings.EndDate.ToShortDateString()}, Offset: {settings.TransactionCount }");

                return new TransactionImportRequest
                {
                    StartDate = settings.StartDate,
                    PayerId = settings.PayerId,
                    EndDate = settings.EndDate,
                    OffsetNumber = settings.TransactionCount
                };
            }
            else
            {
                throw new InvalidOperationException($"Import task for payer {payerId} hasn't been initialized");
            }
        }
    }
}
