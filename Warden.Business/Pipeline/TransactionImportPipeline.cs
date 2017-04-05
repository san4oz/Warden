using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Pipeline.Steps;

namespace Warden.Business.Pipeline
{
    public class TransactionImportPipeline : ITransactionImportPipeline
    {
        public IList<ITransactionImportPipelineStep> Steps { get; set; }

        private IExternalApi api;
        private ITransactionDataProvider provider;
        private ISearchManager search;
        private ICategoryDataProvider categoryProvider;

        public TransactionImportPipeline(IExternalApi api, ITransactionDataProvider provider, ISearchManager search, ICategoryDataProvider categoryProvider)
        {
            this.api = api;
            this.provider = provider;
            this.search = search;
            this.categoryProvider = categoryProvider;
        }

        public void Execute(TransactionImportRequest request)
        {
            RegisterSteps();

            var context = new TransactionImportPipelineContext() { Request = request };

            foreach (var step in this.Steps)
            {
                step.Execute(context);
            }
        }

        protected void RegisterSteps()
        {
            this.Steps = new List<ITransactionImportPipelineStep>();

            this.Steps.Add(new TransactionRetreivingStep(api));
            this.Steps.Add(new TransactionProcessingStep());
            this.Steps.Add(new TransactionCreatingStep(provider));
            this.Steps.Add(new TransactionIndexingStep(search));
        }
    }
}
