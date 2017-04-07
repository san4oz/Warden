using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Core.Extensions;

namespace Warden.DataProvider.DataProviders
{
    public class TransactionDataProvider : BaseDataProvider<Transaction>, ITransactionDataProvider
    {
        public void AttachToCategory(Guid transactionId, Guid categoryId)
        {
            Execute(session =>
            {
                var pair = new TransactionCategory() { CategoryId = categoryId, TransactionId = transactionId };
                session.Save(pair);
                session.Flush();
            });
        }

        public int GetGeneralTransactionCount()
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>().RowCount();                        
            });
        }

        public List<Transaction> GetTransactionsByPayerId(string payerId)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Transaction>().Add(Expression.Eq("PayerId", payerId)).List<Transaction>().ToList();
            });
        }


        public List<Transaction> GetByIdList(Guid[] ids)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Transaction>()
                        .Add(Expression.In("Id", ids))
                        .List<Transaction>().ToList();
            });
        }
       
        public List<Transaction> GetTransactionsByCategoryId(Guid categoryId)
        {
            return Execute(session =>
            {
                var ids = session.CreateCriteria<TransactionCategory>()
                                        .Add(Expression.Eq("CategoryId", categoryId))
                                        .List<TransactionCategory>().Select(tc => tc.TransactionId).ToArray();

                return session.CreateCriteria<Transaction>()
                                    .Add(Expression.In("Id", ids))
                                    .List<Transaction>().ToList();
            });
        }

        public List<Transaction> GetUnprocessedTransactions(Guid[] ids)
        {
            return Execute(session =>
            {
                var withCategoryIds = session.CreateCriteria<TransactionCategory>()
                         .List<TransactionCategory>().Select(tc => tc.TransactionId).ToArray();

                var withoutCategoryIds = ids.Except(withCategoryIds);

                var result = new List<Transaction>();
                foreach(var batch in withoutCategoryIds.Batch<Guid>(1000))
                {
                    var batchData = session.CreateCriteria<Transaction>()
                        .Add(Expression.In("Id", batch.ToArray()))
                        .List<Transaction>().ToList();

                    result.AddRange(batchData);
                }

                return result;
            });
        }

        public List<Transaction> GetProcessedTransactions(Guid categoryId)
        {
            return Execute(session =>
            {
                var ids = session.CreateCriteria<TransactionCategory>()
                            .Add(Expression.Eq("CategoryId", categoryId))
                            .List<TransactionCategory>().Select(tc => tc.TransactionId).ToArray();

                return session.CreateCriteria<Transaction>()
                        .Add(Expression.In("Id", ids))
                        .List<Transaction>().ToList();
            });
        }

        public void Delete(Guid[] ids)
        {
            Execute(session =>
            {
                foreach (var id in ids)
                {
                    var item = session.Get<Transaction>(id);
                    session.Delete(item);
                }

                session.Flush();
            });
        }
    }
}
