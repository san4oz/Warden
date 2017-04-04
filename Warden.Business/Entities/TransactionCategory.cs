using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class TransactionCategory : Entity
    {
        public virtual Guid CategoryId { get; set; }

        public virtual Guid TransactionId { get; set; }
    }
}
