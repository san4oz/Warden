using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ITransactionDataProvider : IDataProvider<Transaction>
    {
        List<Transaction> GetTransactionsByCategoryId(Guid categoryId);

        List<Transaction> GetTransactionsByPayerId(string payerId);

        List<Transaction> GetByIdList(Guid[] ids);

        void Delete(Guid[] ids);

        void DeleteByPayerId(string payerId);

        void AttachToCategory(Guid transactionId, Guid categoryId);

        List<Transaction> GetUnprocessedTransactions(Guid[] ids);

        List<Transaction> GetProcessedTransactions(Guid categoryId);

        int GetGeneralTransactionCount();

        int GetTransactionCountForPayer(string payerId);

        List<Transaction> GetTransactionsToCalibrate(Guid categoryId);

        void MarkAsVoted(Guid transactionId);
    }
}
