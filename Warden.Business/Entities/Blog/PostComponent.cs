using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class PostComponent : Entity
    {
        public virtual ComponentType Type { get; set; }

        public virtual string Data { get; set; }

        public virtual Guid PostId { get; set; }
    }

    public enum ComponentType : int
    {
        Text = 0
    }
}
