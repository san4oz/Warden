using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.SQLDataProvider.DataProviders
{
    public class CategoryDataProvider : SQLDataProvider<Category>, ICategoryProvider
    {
        public Category GetByTitle(string title)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Category>().Add(Restrictions.Eq("Title", title)).UniqueResult<Category>();
            });
        }

        public List<Category> GetByIds(Guid[] ids)
        {
            return Execute(session => 
            {
                return session.QueryOver<Category>()
                                        .Where(c => c.Id.IsIn(ids))
                                        .List().ToList();
            });
        }
    }
}
