using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.DataProvider.DataProviders
{
    public class TransactionDataProvider : BaseDataProvider<Transaction>, ITransactionDataProvider
    {
        public List<Transaction> GetTransactionsByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
