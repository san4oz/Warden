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
        public void SaveUprocessedKeywords(List<UnprocessedKeyword> keywords)
        {
            Execute(session =>
            {
                foreach (var keyword in keywords)
                    session.Save(keyword);

                session.Flush();
            });
        }

        public List<UnprocessedKeyword> GetUprocessedKeywords()
        {
            return Execute(session =>
            {
                return session.CreateCriteria<UnprocessedKeyword>().List<UnprocessedKeyword>().ToList();
            });
        }

        public UnprocessedKeyword GetUprocessedKeyword(Guid id)
        {
            return Execute(session =>
            {
                return session.Get<UnprocessedKeyword>(id);
            });
        }

        public Category GetByTitle(string title)
        {
            return Execute(session =>
            {
                return session.CreateCriteria<Category>().Add(Expression.Eq("Title", title)).UniqueResult<Category>();
            });
        }

        public void DeleteUnprocessedKeyword(Guid id)
        {
            Execute(session =>
            {
                var keyword = session.Get<UnprocessedKeyword>(id);
                session.Delete(keyword);
                session.Flush();
            });
        }
    }
}
