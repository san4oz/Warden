using System;
using Warden.Business.Import;

namespace Warden.Business.Entities
{
    public class TransactionImportSettings : Entity
    {
        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual string PayerId { get; set; }

        public virtual ImportTaskStatus Status { get; set; }

        public virtual int TransactionCount { get; set; }
    }
}
