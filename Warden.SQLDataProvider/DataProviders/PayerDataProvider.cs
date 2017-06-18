using System;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.SQLDataProvider.DataProviders
{
    public class PayerDataProvider : SQLDataProvider<Payer>, IPayerProvider
    {
        public Payer Get(string payerId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Payer>().Where(p => p.PayerId == payerId).SingleOrDefault();
            });
        }
    }
}
