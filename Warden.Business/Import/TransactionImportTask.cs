﻿using System;
using System.Collections.Concurrent;
using Warden.Business.Utils;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Business.Import.Processor;
using Warden.Business.Providers;
using System.Collections.Generic;

namespace Warden.Business.Import
{
    public class TransactionImportTask : ITransactionImportTask
    {
        private readonly ImportSettingsManager settingsManager;
        private readonly TransactionImportProcessor importProcessor;
        private readonly TransactionManager transactionManager;

        public TransactionImportSettings Settings { get; set; }

        public TransactionImportTask(
               ImportSettingsManager settingsManager,
               TransactionImportProcessor importProcessor,
               TransactionManager transactionManager)
        {
            this.settingsManager = settingsManager;
            this.importProcessor = importProcessor;
            this.transactionManager = transactionManager;
        }

        public void Execute(string payerId, bool rebuild = false)
        {
            if (string.IsNullOrEmpty(payerId))
                throw new ArgumentNullException();

            InitializeTaskSettings(payerId);

            if (rebuild)
            {
                CleanUpPayerTransactions();
                OnRebuildStart();
            }

            Import();
        }

        protected void Import()
        {
            OnImportStart();
            UpdateTaskStatus(ImportTaskStatus.InProgress);

            try
            {
                do
                {
                    importProcessor.Execute(BuildImportRequest());

                } while (ShouldContinue());

                OnTaskFinished();
            }
            catch
            {
                OnTaskFailed();
                UpdateTaskStatus(ImportTaskStatus.Failed);
            }
        }

        protected void CleanUpPayerTransactions()
        {
            transactionManager.DeleteByPayerId(Settings.PayerId);
            UpdateTransactionsCount(0);
        }

        protected bool ShouldContinue()
        {
            var actualTransactionsCount = transactionManager.GetCountByPayerId(Settings.PayerId);

            if(Settings.TransactionCount >= actualTransactionsCount)
            {
                UpdateTaskStatus(ImportTaskStatus.Finished);
                return false;
            }

            UpdateTransactionsCount(actualTransactionsCount);
            return true;
        }

        protected void InitializeTaskSettings(string payerId)
        {
            Settings = settingsManager.GetByPayerId(payerId);

            if (Settings.Status == ImportTaskStatus.InProgress)
            {
                UpdateTaskStatus(ImportTaskStatus.Failed);
            }
        }

        protected void UpdateTransactionsCount(int count)
        {
            Settings.TransactionCount = count;
        }

        protected void UpdateTaskStatus(ImportTaskStatus status)
        {
            settingsManager.UpdateTaskStatus(Settings, status);
            OnTaskStatusUpdated(status);
        }

        protected TransactionImportRequest BuildImportRequest()
        {
            return new TransactionImportRequest
            {
                StartDate = Settings.StartDate,
                PayerId = Settings.PayerId,
                EndDate = Settings.EndDate,
                OffsetNumber = Settings.TransactionCount
            };
        }


#region events
        public void OnRebuildStart()
        {
        }

        public void OnImportStart()
        {
        }

        public void OnTaskFinished()
        {
        }

        public void OnTaskFailed()
        {
        }

        public void OnTaskStatusUpdated(ImportTaskStatus status)
        {
        }
#endregion events
    }
}
