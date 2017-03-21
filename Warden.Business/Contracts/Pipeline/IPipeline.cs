using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Contracts.Pipeline
{
    public interface IPipeline
    {
        void Execute(IPipelineContext context);

        IList<IPipelineStep> Steps { get; set; }
    }
}
