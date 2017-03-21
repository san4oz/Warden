using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Contracts.Pipeline
{
    public interface IPipelineStep
    {
        void Execute(IPipelineContext context);
    }
}
