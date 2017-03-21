using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Api;
using Warden.Business.Entities;
using Warden.Core.Utils.Tokenizer;

namespace Warden.Business.Api
{
    public class TransactionTextProcessor : ITransactionTextProcessor
    {
        public void MakeUpKeywords(IEnumerable<Transaction> transactions)
        {
            var textNormalizer = new TextNormalizer();

            foreach (var item in transactions)
            {
                item.Keywords = textNormalizer.Normalize(item.Keywords);
            }
        }
    }
}
