using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Business.Pipeline.Steps
{
    public class KeywordsExtractingStep : IPipelineStep
    {
        private ICategoryDataProvider categoryProvider;
        private TransactionImportPipelineContext context;

        public KeywordsExtractingStep(ICategoryDataProvider categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }

        public void Execute(IPipelineContext context)
        {
            this.context = (TransactionImportPipelineContext)context;

            var keywords = this.context.Items.SelectMany(i => i.Keywords
                                .Split(new[] { ';' })).Where(k => k.Length > Constants.Business.MinKeywordLength).Distinct();

            categoryProvider.SaveUprocessedKeywords(keywords.Select(k => new UnprocessedKeyword() { Value = k }).ToList());
        }
    }
}
