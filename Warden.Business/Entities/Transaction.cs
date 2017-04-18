using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Transaction : Entity
    {
        public virtual string PayerId { get; set; }

        public virtual string ReceiverId { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string Keywords { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string ExternalId { get; set; }
    }
}
