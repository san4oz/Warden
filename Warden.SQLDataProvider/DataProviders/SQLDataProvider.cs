using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.SQLDataProvider.DataProviders
{
    public abstract class SQLDataProvider<T> : IProvider<T>
        where T : Entity, new()
    {
        public virtual List<T> All()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<T>();

                return criteria.List<T>().ToList();
            });
        }

        public virtual void Delete(Guid id)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    var valueToBeRemoved = session.Get<T>(id);
                    if (valueToBeRemoved != null)
                    {
                        session.Delete(valueToBeRemoved);
                        transaction.Commit();
                    }
                }
            });
        }

        public T Get(Guid id)
        {
            return Execute<T>(session =>
            {
                return session.Get<T>(id);
            });
        }

        public virtual void Save(T entity)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            });
        }

        public virtual void Update(T entity)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            });
        }

        #region helpers
        protected T Execute<T>(Func<ISession, T> expression)
        {
            using (var session = NhibernateSessionHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected void Execute(Action<ISession> expression)
        {
            using (var session = NhibernateSessionHelper.OpenSession())
            {
                expression(session);
            }
        }      
        #endregion
    }
}
