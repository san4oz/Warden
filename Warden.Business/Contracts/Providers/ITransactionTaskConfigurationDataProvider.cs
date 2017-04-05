using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface ITransactionTaskConfigurationDataProvider : IDataProvider<TransactionImportTaskConfiguration>
    {
        TransactionImportTaskConfiguration GetForPayer(string payerId);

        TransactionImportTaskConfiguration GetDefault(string payerId);
    }
}
