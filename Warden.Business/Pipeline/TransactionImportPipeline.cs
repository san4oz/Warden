using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Api;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Pipeline.Steps;

namespace Warden.Business.Pipeline
{
    public class TransactionImportPipeline : ITransactionImportPipeline
    {
        public IList<IPipelineStep> Steps { get; set; }

        private IExternalApi api;
        private ITransactionDataProvider provider;
        private ISearchManager search;
        private ITransactionTextProcessor textProcessor;
        private ICategoryDataProvider categoryProvider;

        public TransactionImportPipeline(IExternalApi api, ITransactionDataProvider provider, ISearchManager search, ITransactionTextProcessor textProcessor, ICategoryDataProvider categoryProvider)
        {
            this.api = api;
            this.provider = provider;
            this.search = search;
            this.textProcessor = textProcessor;
            this.categoryProvider = categoryProvider;
        }

        public void Execute(IPipelineContext context)
        {
            RegisterSteps();

            foreach (var step in this.Steps)
            {
                step.Execute(context);
            }
        }

        protected void RegisterSteps()
        {
            this.Steps = new List<IPipelineStep>();

            this.Steps.Add(new TransactionRetreivingStep(api));
            this.Steps.Add(new TransactionProcessingStep(textProcessor));
            this.Steps.Add(new TransactionCreatingStep(provider));
            this.Steps.Add(new TransactionIndexingStep(search));
            this.Steps.Add(new KeywordsExtractingStep(categoryProvider));
        }
    }
}
