﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Contracts.Scheduler
{
    public interface ITransactionExtractionTask : ITask
    {
        void RunExect(string payerId);
        void RunAll();
    }
}