using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Category : Entity
    {
        public virtual string Title { get; set; }

        public virtual string Keywords { get; set; }
    }
}
