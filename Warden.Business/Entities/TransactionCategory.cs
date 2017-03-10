using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class TransactionCategory : Entity
    {
        public Guid CategoryId { get; set; }

        public Guid TransactionId { get; set; }
    }
}
