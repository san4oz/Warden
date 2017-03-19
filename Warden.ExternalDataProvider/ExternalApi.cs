using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Core.Readers;
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

        public async Task<IList<Transaction>> GetTransactionsAsync(TransactionRequest request)
        {
            return await transactionProvider.GetTransactions(request);
        }
    }
}
