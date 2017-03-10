using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface ITransactionDataProvider : IDataProvider<Transaction>
    {
        List<Transaction> GetTransactionsByCategoryId(Guid categoryId);
    }
}
