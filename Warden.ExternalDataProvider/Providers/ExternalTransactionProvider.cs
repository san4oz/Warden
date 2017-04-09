﻿using System;
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
            var webRequest = new Dictionary<string, string>()
            {
                { "from", request.From.ToString("yyyy-MM-dd") },
                { "to", request.To.ToString("yyyy-MM-dd") },
                { "who", request.PayerId },
                { "offset", request.OffsetNumber.ToString() }
            };

            var transactionsData = await GetEntitiesAsync(webRequest);

            if (transactionsData == null)
                return new List<Transaction>();

            return transactionsData.Select(td => td.ToWardenTransaction()).ToList();
        }
    }
}
