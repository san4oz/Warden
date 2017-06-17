using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IPostProvider : IProvider<Post>
    {
        void SaveWithComponents(Post post, IEnumerable<PostComponent> components);

        Post GetWithComponents(Guid id);
    }
}
