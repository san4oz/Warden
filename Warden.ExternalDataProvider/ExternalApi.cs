using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Business.Api;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.ExternalDataProvider.Providers;

namespace Warden.ExternalDataProvider
{
    public class ExternalApi : IExternalApi
    {
        private ExternalTransactionProvider transactionProvider;

        public ExternalApi()
        {
            this.transactionProvider = new ExternalTransactionProvider();
        }

        public async Task<IList<Transaction>> GetTransactionsAsync(TransactionRetreivingRequest request)
        {
            return await transactionProvider.GetTransactions(request);
        }
    }
}
