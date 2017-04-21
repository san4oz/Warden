using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Core.Extensions;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class TransactionDataProvider : BaseDataProvider<Transaction>, ITransactionDataProvider
    {
        public void AttachToCategory(Guid transactionId, Guid categoryId)
        {
            Execute(session =>
            {
                var transaction = session.Get<Transaction>(transactionId);
                transaction.CategoryId = categoryId;
                session.SaveOrUpdate(transaction);
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
                return session.QueryOver<Transaction>()
                            .Where(t => t.PayerId == payerId).List().ToList();
            });
        }


        public List<Transaction> GetByIdList(Guid[] ids)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>()
                            .Where(t => t.Id.IsIn(ids)).List().ToList();
            });
        }
       
        public List<Transaction> GetTransactionsByCategoryId(Guid categoryId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>()
                                .Where(t => t.CategoryId == categoryId)
                                .List().ToList();
            });
        }

        public List<Transaction> GetUnprocessedTransactions(Guid[] ids)
        {
            return Execute(session =>
            {
                var result = new List<Transaction>();

                foreach(var batch in ids.Batch(1000))
                {
                    var data = session.QueryOver<Transaction>()
                                        .Where(t => t.Id.IsIn(ids))
                                        .Where(t => t.CategoryId == null)
                                        .List().ToList();

                    result.AddRange(data);
                }

                return result;
            });
        }

        public List<Transaction> GetTransactionsToCalibrate(Guid categoryId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>()
                            .Where(t => !t.Voted)
                            .Where(t => t.CategoryId == categoryId)
                            .List().ToList();
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

        public void DeleteByPayerId(string payerId)
        {
            if (string.IsNullOrEmpty(payerId))
                return;

            Execute(session => 
            {
                ITransaction transaction = null;
                try
                {
                    transaction = session.BeginTransaction();
                    var query = session.CreateQuery("DELETE Transaction t WHERE t.PayerId like :id")
                            .SetParameter("id", payerId, NHibernateUtil.String);
                    
                    query.ExecuteUpdate();
                    transaction.Commit();
                }
                catch
                {
                    if (transaction != null)
                        transaction.Rollback();
                    throw;
                }
            });
        }

        public int GetTransactionCountForPayer(string payerId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>().Where(t => t.PayerId == payerId).RowCount();
            });
        }

        public void MarkAsVoted(Guid transactionId)
        {
            Execute(session =>
            {
                var transaction = session.Get<Transaction>(transactionId);
                transaction.Voted = true;
                session.SaveOrUpdate(transaction);
                session.Flush();
            });
        }
    }
}
