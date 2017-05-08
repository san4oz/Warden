using System;
using System.Collections.Concurrent;
using Warden.Business.Helpers;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Business.Import.Pipeline;
using System.Collections.Generic;
using System.Linq;

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

        public ImportTaskStatus StartImportForPayer(string payerId, bool rebuild = false)
        {
            //bool shouldContinue(string payer)
            //{
            //    if (Settings.TryGetValue(payerId, out TransactionImportSettings settings) && ShouldTryToImportMore(payer, settings))
            //    {
            //        UpdateItemsCount(payer, settings);
            //        return true;
            //    }
            //    return false;
            //};

            if (string.IsNullOrEmpty(payerId))
                return ImportTaskStatus.Failed;
            InitializeTaskForPayer(payerId);

            var temporaryTransactions = transactionManager.GetTransactionsByPayerId(payerId);
            try
            {
                OnTaskStarted(payerId, rebuild);
                var request = BuildImportRequest(payerId, rebuild);
                importPipeline.Execute(request);

                //API returns us all records 
                //so offsets not needed anymore (should be checked and refactored)
                //while (true)
                //{
                //    var request = BuildImportRequest(payerId, rebuild);
                //    importPipeline.Execute(request);

                //    rebuild = false;
                //    if (!shouldContinue(payerId))
                //        break;
                //}
            }
            catch(Exception ex)
            {
                OnTaskFailed(temporaryTransactions, payerId, ex.StackTrace);
                return ImportTaskStatus.Failed;
            }
            OnTaskFinished(payerId);
            return ImportTaskStatus.Finished;
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

        public void InitializeTaskForPayer(string payerId)
        {            
            if(!Settings.ContainsKey(payerId))
            {
                var settings = settingsManager.GetByPayerId(payerId);

                Settings.TryAdd(payerId, settings);               
            }
        }

        protected void OnTaskStarted(string payerId, bool rebuild)
        {
            TransactionImportTracer.Trace(payerId, rebuild ? "Rebuild task was started." : "Import task was started.");
            if (rebuild)
            {
                transactionManager.DeleteByPayerId(payerId);
                UpdateItemsCount(payerId);
            }
            UpdateTaskStatus(payerId, ImportTaskStatus.InProgress);
        }

        protected void OnTaskFailed(IEnumerable<Transaction> transactions, string payerId, string stackTrace)
        {
            TransactionImportTracer.Trace(payerId, $"Task was failed. Stack trace: {Environment.NewLine} {stackTrace}");
            if (transactions != null && transactions.Any())
            {
                transactionManager.DeleteByPayerId(payerId);
                foreach (var transaction in transactions)
                {
                    transactionManager.Save(transaction);
                }
            }
            UpdateTaskStatus(payerId, ImportTaskStatus.Failed);
        }

        protected void OnTaskFinished(string payerId)
        {
            TransactionImportTracer.Trace(payerId, $"Task has been successfully finished.");
            UpdateTaskStatus(payerId, ImportTaskStatus.Finished);
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

        protected TransactionImportRequest BuildImportRequest(string payerId, bool rebuild)
        {
            if(Settings.TryGetValue(payerId, out TransactionImportSettings settings))
            {
                TransactionImportTracer.Trace(payerId, $"Request: StartDate: {settings.StartDate.ToShortDateString()}, ToDate: {settings.EndDate.ToShortDateString()}, Offset: {settings.TransactionCount }");

                return new TransactionImportRequest
                {
                    StartDate = settings.StartDate,
                    PayerId = settings.PayerId,
                    EndDate = settings.EndDate,
                    OffsetNumber = settings.TransactionCount,
                    Rebuild = rebuild
                };
            }
            else
            {
                throw new InvalidOperationException($"Import task for payer {payerId} hasn't been initialized");
            }
        }
    }
}
