using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Import
{
    public interface ITransactionImportTask
    {
        TransactionImportSettings Settings { get; set; }

        void Execute(string payerId, bool rebuild = false);

        void OnRebuildStart();

        void OnImportStart();

        void OnTaskFinished();

        void OnTaskFailed();

        void OnTaskStatusUpdated(ImportTaskStatus status);
    }
}
