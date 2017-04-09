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

        List<Transaction> GetTransactionsByPayerId(string payerId);

        List<Transaction> GetByIdList(Guid[] ids);

        void Delete(Guid[] ids);

        void AttachToCategory(Guid transactionId, Guid categoryId);

        List<Transaction> GetUnprocessedTransactions(Guid[] ids);

        List<Transaction> GetProcessedTransactions(Guid categoryId);

        int GetGeneralTransactionCount();

        int GetTransactionCountForPayer(string payerId);
    }
}
