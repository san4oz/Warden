using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.SQLDataProvider.DataProviders
{
    public class PayerDataProvider : SQLDataProvider<Payer>, IPayerProvider
    {
    }
}
