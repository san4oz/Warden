﻿using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ITransactionDataProvider : IDataProvider<Transaction>
    {
        List<Transaction> GetByCategoryId(Guid categoryId);

        List<Transaction> GetByPayerId(string payerId);

        List<Transaction> GetByIdList(Guid[] ids);

        void Delete(Guid[] ids);

        void DeleteByPayerId(string payerId);

        void AttachToCategory(Guid transactionId, Guid categoryId);

        List<Transaction> GetWithoutCategory(Guid[] ids);

        int GetTotalCount();

        int GetCountByPayerId(string payerId);

        List<Transaction> GetNotVoted(Guid categoryId);

        void MarkAsVoted(Guid transactionId);
    }
}
