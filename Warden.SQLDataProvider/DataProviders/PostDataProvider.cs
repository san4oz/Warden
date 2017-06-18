using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.SQLDataProvider.DataProviders
{
    public class PostProvider : SQLDataProvider<Post>, IPostProvider
    {
        public void SaveWithComponents(Post post, IEnumerable<PostComponent> components)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(post);

                    foreach (var component in components)
                    {
                        session.Save(component);
                    }

                    session.Flush();
                    transaction.Commit();
                }
            });
        }

        public Post GetWithComponents(Guid id)
        {
            return Execute(session =>
            {
                var result = session.CreateCriteria<Post>().Add(Restrictions.Eq("Id", id)).UniqueResult<Post>();
                var components = session.CreateCriteria<PostComponent>().Add(Restrictions.Eq("PostId", id))
                    .List<PostComponent>();
                result.Components = components;
                return result;
            });
        }
    }
}