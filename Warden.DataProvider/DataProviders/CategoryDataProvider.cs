using NHibernate.Criterion;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class CategoryDataProvider : BaseDataProvider<Category>, ICategoryProvider
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
