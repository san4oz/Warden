using NHibernate.Criterion;
using System;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class PayerDataProvider : BaseDataProvider<Payer>, IPayerProvider
    {
        public Payer Get(string payerId)
        {
            return Execute<Payer>(session =>
            {
                return session.QueryOver<Payer>()
                                .Where(payer => payer.PayerId == payerId)
                                .SingleOrDefault();
            });
        }
    }
}
