using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Core.Extensions;

namespace Warden.Business.Contracts.Managers
{
    public class AnalysisManager
    {
        private readonly ITransactionDataProvider transactionProvider;
        private readonly ICategoryDataProvider categoryProvider;
        private readonly IKeywordDataProvider keywordProvider;

        public AnalysisManager(ITransactionDataProvider transactionProvider, ICategoryDataProvider categoryProvider, IKeywordDataProvider keywordProvider)
        {
            this.transactionProvider = transactionProvider;
            this.categoryProvider = categoryProvider;
            this.keywordProvider = keywordProvider;
        }

        public bool AttachToCategory(Guid transactionId, Guid categoryId)
        {
            if (transactionId.IsEmpty() || categoryId.IsEmpty())
                return false;

            var keyword = transactionProvider.Get(transactionId);
            var category = categoryProvider.Get(categoryId);

            transactionProvider.AttachToCategory(transactionId, categoryId);

            return true;
        }

        public void VoteForKeyword(Guid categoryId, string keywordText, bool correct)
        {
            var keyword = keywordProvider.Get(keywordText, categoryId);
            if (keyword == null)
            {
                keyword = new CategoryKeyword() { CategoryId = categoryId, Keyword = keywordText };
            }
            keyword.SuccessVotes += correct ? 1 : 0;
            keyword.TotalVotes += 1;

            keywordProvider.Save(keyword);
        }

        public void MarkAsVoted(Guid transactionId) => transactionProvider.MarkAsVoted(transactionId);
    }
}
