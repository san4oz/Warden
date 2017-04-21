using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerProvider>
    {
        public PayerManager(IPayerProvider provider) : base(provider)
        {

        }
    }
}
