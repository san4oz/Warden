using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerDataProvider>
    {
        IPayerDataProvider provider;

        public PayerManager(IPayerDataProvider provider) : base(provider)
        {
            this.provider = provider;
        }

        public Payer Get(string payerId) => provider.Get(payerId);
    }
}
