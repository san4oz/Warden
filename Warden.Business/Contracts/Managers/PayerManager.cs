using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Managers
{
    public class PayerManager : EntityManager<Payer, IPayerDataProvider>
    {
        public PayerManager(IPayerDataProvider provider) : base(provider)
        {

        }
    }
}
