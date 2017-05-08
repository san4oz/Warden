using Warden.Business.Entities;
using Warden.Business.Import;
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

<<<<<<< HEAD
        public override void Save(Payer entity)
        {
            base.Save(entity);
            IoC.Resolve<TransactionImportTask>().InitializeTaskForPayer(entity.PayerId);
        }
=======
        public Payer Get(string payerId) => provider.Get(payerId);
>>>>>>> Chart
    }
}
