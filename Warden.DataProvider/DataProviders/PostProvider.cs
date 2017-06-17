using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.DataProvider.DataProviders
{
    public class PostProvider : BaseDataProvider<Post>, IPostProvider
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
    }
}
