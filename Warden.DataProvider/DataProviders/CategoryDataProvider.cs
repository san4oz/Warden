using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.DataProvider.DataProviders
{
    public class CategoryDataProvider : BaseDataProvider<Category>, ICategoryDataProvider
    {
        public Category GetByTitle(string title)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Category>().Add(Expression.Eq("Title", title)).UniqueResult<Category>();
            });
        }
    }
}
