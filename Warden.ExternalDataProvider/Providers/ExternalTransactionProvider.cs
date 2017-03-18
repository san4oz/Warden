using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.ExternalDataProvider.Entities;
using Warden.ExternalDataProvider;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.ExternalDataProvider.Providers
{
    public class ExternalTransactionProvider : ExternalDataProviderBase<TransactionData>
    {
        protected override string BaseUri
        {
            get { return "www.007.org.ua/api/export-transactions-with-params"; }
        }

        public IList<Transaction> GetTransactions(TransactionRequest request)
        {
            var webRequest = new Dictionary<string, string>()
            {
                { "from", request.From.ToString("yyyy-mm-dd") },
                { "to", request.To.ToString("yyyy-mm-dd") },
                { "who", request.PayerId }
            };

            var transactionsData = GetEntities(webRequest).Result;
            if (transactionsData == null)
                return new List<Transaction>();

            return transactionsData.Select(t => t.ToWardenTransaction()).ToList();
        }
    }
}
