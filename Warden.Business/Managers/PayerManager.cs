using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerDataProvider>
    {
        public PayerManager(IPayerDataProvider provider) : base(provider)
        {

        }
    }
}
