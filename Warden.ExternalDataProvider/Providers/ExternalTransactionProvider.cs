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
            get { return "http://www.007.org.ua/api/export-transactions-with-params"; }
        }

        public async Task<IList<Transaction>> GetTransactions(TransactionRetreivingRequest request)
        {
            int offset = 0;
            int count = 0;
            var result = new List<Transaction>();

            do
            {
                count = result.Count();

                var webRequest = new Dictionary<string, string>()
                {
                    { "from", request.From.ToString("yyyy-mm-dd") },
                    { "to", request.To.ToString("yyyy-mm-dd") },
                    { "who", request.PayerId },
                    { "offset", offset.ToString() }
                };

                var transactionsData = await GetEntitiesAsync(webRequest);
                result.AddRange(transactionsData.Select(td => td.ToWardenTransaction()).ToList());

                offset++;

            } while (result.Count() > count);

            if (result == null)
                return new List<Transaction>();

            return result;
        }
    }
}
