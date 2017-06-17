using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Post : Entity
    {
        public virtual string Title { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual string Banner { get; set; }

        public virtual IEnumerable<PostComponent> Components { get; set; }
    }
}
