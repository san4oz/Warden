using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Pipeline;

namespace Warden.Business.Contracts.Pipeline
{
    public interface ITransactionImportPipeline
    {
        void Execute(TransactionImportRequest request);
    }
}
