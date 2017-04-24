using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Providers
{
    public interface ITransactionSourceProvider
    {
        Task<IList<Transaction>> GetAsync(TransactionRequest request);
    }
}
