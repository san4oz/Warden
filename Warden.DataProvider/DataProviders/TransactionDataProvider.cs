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
    public class TransactionDataProvider : BaseDataProvider<Transaction>, ITransactionProvider
    {
        public void AttachToCategory(List<Guid> ids, Guid categoryId)
        {
            Execute(session =>
            {
                var transactions = session.QueryOver<Transaction>()
                            .Where(t => t.Id.IsIn(ids)).List().ToList();


                foreach(var transaction in transactions)
                {
                    transaction.CategoryId = categoryId;
                    session.SaveOrUpdate(transaction);
                }

                session.Flush();
            });
        }

        public int GetTotalCount()
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>().RowCount();                        
            });
        }

        public List<Transaction> GetByPayerId(string payerId)
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
       
        public List<Transaction> GetByCategoryId(Guid categoryId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>()
                                .Where(t => t.CategoryId == categoryId)
                                .List().ToList();
            });
        }

        public List<Transaction> GetWithoutCategory(Guid[] ids)
        {
            return Execute(session =>
            {
                var result = new List<Transaction>();

                foreach(var batch in ids.Batch(1000))
                {
                    var data = session.QueryOver<Transaction>()
                                        .Where(t => t.Id.IsIn(batch.ToArray()))
                                        .Where(t => t.CategoryId == null)
                                        .List().ToList();

                    result.AddRange(data);
                }

                return result;
            });
        }

        public List<Transaction> GetWithoutCategory()
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>().Where(t => t.CategoryId == null)
                                                       .Take(100).List().ToList();
            });
        }

        public List<Transaction> GetNotVoted(Guid categoryId)
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
            Execute(session => 
            {
                using (var transaction = session.BeginTransaction())
                {
                    var query = session.CreateQuery("DELETE Transaction t WHERE t.PayerId like :id")
                           .SetParameter("id", payerId, NHibernateUtil.String);

                    query.ExecuteUpdate();
                    transaction.Commit();
                }
            });
        }

        public int GetCountByPayerId(string payerId)
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

        public List<Transaction> GetByGroupId(string groupId)
        {
            return Execute(session =>
            {
                return session.QueryOver<Transaction>().Where(t => t.GroupId == groupId).List().ToList();
            });
        }
    }
}
