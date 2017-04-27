using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerProvider>
    {
        IPayerProvider provider;

        public PayerManager(IPayerProvider provider) : base(provider)
        {
            this.provider = provider;
        }

        public Payer Get(string payerId) => provider.Get(payerId);
    }
}
