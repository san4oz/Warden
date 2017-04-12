using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Import;

namespace Warden.Business.Contracts.Scheduler
{
    public interface ITransactionImportTask
    {
        ImportTaskStatus StartImport(string payerId = null, bool rebuild = false);

        void Initialize();
    }
}
