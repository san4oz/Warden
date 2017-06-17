using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ITransactionProvider : IProvider<Transaction>
    {
        List<Transaction> GetByCategoryId(Guid categoryId);

        List<Transaction> GetByPayerId(string payerId);

        List<Transaction> GetByIdList(Guid[] ids);

        void Delete(Guid[] ids);

        void DeleteByPayerId(string payerId);

        void AttachToCategory(List<Guid> ids, Guid categoryId);

        List<Transaction> GetWithoutCategory(Guid[] ids);

        int GetTotalCount();

        int GetCountByPayerId(string payerId);

        List<Transaction> GetNotVoted(Guid categoryId);

        void MarkAsVoted(Guid transactionId);

        List<Transaction> GetWithoutCategory();

        List<Transaction> GetByGroupId(string groupId);
    }
}
