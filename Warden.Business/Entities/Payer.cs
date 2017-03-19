using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Payer : Entity
    {
        public virtual string PayerId { get; set; }

        public virtual string Name { get; set; }
    }
}
