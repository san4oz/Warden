using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Utils;

namespace Warden.Business.Import
{
    public class TransactionImportTaskLogger : ITransactionImportTask
    {
        private readonly ITransactionImportTask importTask;

        public TransactionImportTaskLogger(ITransactionImportTask importTask)
        {
            this.importTask = importTask;
        }

        public TransactionImportSettings Settings { get; set; }

        public void Execute(string payerId, bool rebuild = false)
        {
            this.importTask.Execute(payerId, rebuild);
        }

        public void OnImportStart()
        {
            TransactionImportTracer.Trace(importTask.Settings.PayerId, "Import task has been started.");
        }

        public void OnRebuildStart()
        {
            TransactionImportTracer.Trace(importTask.Settings.PayerId, "Cleaning up old data...");
        }

        public void OnTaskFailed()
        {
            TransactionImportTracer.Trace(importTask.Settings.PayerId, "Task has been failed.");
        }

        public void OnTaskFinished()
        {
            TransactionImportTracer.Trace(importTask.Settings.PayerId, "Import task has been finished");
        }

        public void OnTaskStatusUpdated(ImportTaskStatus status)
        {
            TransactionImportTracer.Trace(importTask.Settings.PayerId, $"Task status was updated to {status.GetStringRepresentation()}");
        }
    }
}
