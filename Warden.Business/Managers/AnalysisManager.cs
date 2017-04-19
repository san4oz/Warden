using System;
using Warden.Business.Entities;
using Warden.Business.Helper;
using Warden.Business.Providers;
using Warden.Core.Extensions;

namespace Warden.Business.Managers
{
    public class AnalysisManager
    {
        private readonly TransactionManager transactionManager;
        private readonly IKeywordDataProvider keywordProvider;

        public AnalysisManager()
        {
            this.transactionManager = IoC.Resolve<TransactionManager>();
            this.keywordProvider = IoC.Resolve<IKeywordDataProvider>();
        }

        public bool AttachToCategory(Guid transactionId, Guid categoryId)
        {
            if (transactionId.IsEmpty() || categoryId.IsEmpty())
                return false;

            transactionManager.AttachToCategory(transactionId, categoryId);

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

        public bool TryAttachToCategory(Transaction transaction)
        {
            var keywords = transaction.Keywords.Split(Constants.Keywords.Separator);

            foreach(var keyword in keywords)
            {
                var trustedKeyword = TrustHelper.GetTheMostTrusted(keywordProvider.Get(keyword));
                if (trustedKeyword != null)
                {
                    transactionManager.AttachToCategory(transaction.Id, trustedKeyword.CategoryId);
                    MarkTransactionAsVoted(transaction.Id);
                    return true;
                }
            }

            return false;
        }

        public void MarkTransactionAsVoted(Guid transactionId) => transactionManager.MarkAsVoted(transactionId);
    }
}
