using System;

namespace Warden.Business.Entities
{
    public class TransactionCategory : Entity
    {
        public virtual Guid CategoryId { get; set; }

        public virtual Guid TransactionId { get; set; }

        public virtual bool Voted { get; set; }
    }
}
