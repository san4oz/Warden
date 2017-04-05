using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;
using Warden.Business.Entities;

namespace Warden.DataProvider.DataProviders
{
    public class TransactionTaskConfigurationDataProvider
        : BaseDataProvider<TransactionImportTaskConfiguration>, ITransactionTaskConfigurationDataProvider
    {
        public TransactionImportTaskConfiguration GetDefault(string payerId)
        {
            return new TransactionImportTaskConfiguration()
            {
                PayerId = payerId,
                StartDate = new DateTime(2016, 1, 1),
                EndDate = DateTime.Now
            };
        }

        public TransactionImportTaskConfiguration GetForPayer(string payerId)
        {
            return Execute(session =>
            {
                var result = session
                    .CreateCriteria<TransactionImportTaskConfiguration>()
                        .Add(Expression.Eq("PayerId", payerId))
                        .UniqueResult<TransactionImportTaskConfiguration>();

                return result ?? GetDefault(payerId);
            });
        }
    }
}
