using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.Blog.Components
{
    public abstract class Component : Entity
    {
        public virtual string PostId { get; set; }

        public abstract string Name { get; }

        public abstract string Content { get; set; }
    }
}
