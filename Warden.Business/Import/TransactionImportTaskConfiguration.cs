using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;
using Warden.Business.Import;

namespace Warden.Business.Entities
{
    public class TransactionImportTaskConfiguration : Entity
    {
        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual string PayerId { get; set; }

        public virtual ImportTaskStatus Status { get; set; }

        public virtual int TransactionCount { get; set; }
    }
}
