using Warden.Business.Entities;
using Warden.Business.Import;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerProvider>
    {
        public PayerManager(IPayerProvider provider) : base(provider)
        {

        }

        public override void Save(Payer entity)
        {
            base.Save(entity);
            IoC.Resolve<TransactionImportTask>().InitializeTaskForPayer(entity.PayerId);
        }
    }
}
