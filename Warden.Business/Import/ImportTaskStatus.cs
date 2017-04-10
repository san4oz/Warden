using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Import
{
    public enum ImportTaskStatus : int
    {
        NotStarted = 0,
        InProgress = 1,
        Finished = 2,
        Failed = 3
    }
}
