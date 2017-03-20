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
        : BaseDataProvider<TransactionExtractionTaskConfiguration>, ITransactionTaskConfigurationDataProvider
    {
        public TransactionExtractionTaskConfiguration GetDefault(string payerId)
        {
            return new TransactionExtractionTaskConfiguration()
            {
                PayerId = payerId,
                StartDate = new DateTime(2016, 1, 1),
                EndDate = DateTime.Now
            };
        }

        public TransactionExtractionTaskConfiguration GetForPayer(string payerId)
        {
            return Execute(session =>
            {
                var result = session
                    .CreateCriteria<TransactionExtractionTaskConfiguration>()
                        .Add(Expression.Eq("PayerId", payerId))
                        .UniqueResult<TransactionExtractionTaskConfiguration>();

                return result ?? GetDefault(payerId);
            });
        }
    }
}
