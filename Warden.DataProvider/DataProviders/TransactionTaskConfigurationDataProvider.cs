using NHibernate.Criterion;
using System;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class ImportSettingsProvider
        : BaseDataProvider<TransactionImportSettings>, IImportSettingsProvider
    {
        public TransactionImportSettings GetByPayerId(string payerId)
        {
            return Execute(session =>
            {
                var result = session.QueryOver<TransactionImportSettings>()
                                .Where(setting => setting.PayerId == payerId)
                                .SingleOrDefault();

                return result ?? CreateDefault(payerId);
            });
        }

        private TransactionImportSettings CreateDefault(string payerId)
        {
            return new TransactionImportSettings()
            {
                PayerId = payerId,
                StartDate = new DateTime(2016, 1, 1),
                EndDate = DateTime.Now
            };
        }
    }
}
