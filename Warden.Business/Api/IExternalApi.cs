using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Api
{
    public interface IExternalApi
    {
        Task<IList<Transaction>> GetTransactionsAsync(TransactionRetreivingRequest request);
    }
}
