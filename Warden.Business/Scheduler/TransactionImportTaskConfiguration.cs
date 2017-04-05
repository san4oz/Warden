using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;

namespace Warden.Business.Entities
{
    public class TransactionImportTaskConfiguration : Entity, ITaskConfiguration
    {
        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual string PayerId { get; set; }
    }
}
