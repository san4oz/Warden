using System;
using Warden.Business.Entities;
using Warden.Business.Helpers;
using Warden.Business.Providers;
using Warden.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Warden.Business.Managers
{
    public class AnalysisManager
    {
        private readonly TransactionManager transactionManager;
        private readonly IKeywordProvider keywordProvider;

        public AnalysisManager()
        {
            this.transactionManager = IoC.Resolve<TransactionManager>();
            this.keywordProvider = IoC.Resolve<IKeywordProvider>();
        }

        public bool AttachToCategory(Guid transactionId, Guid categoryId)
        {
            if (transactionId.IsEmpty() || categoryId.IsEmpty())
                return false;

            var transaction = transactionManager.Get(transactionId);
            if (transaction == null)
                return false;

            if (!transaction.GroupId.IsEmpty())
            {
                var duplicateTransactions = transactionManager.GetByGroupId(transaction.GroupId);
                transactionManager.AttachToCategory(duplicateTransactions.Select(t => t.Id).ToList(), categoryId);
                return true;
            }
           
            transactionManager.AttachToCategory(new List<Guid>() { transactionId }, categoryId);

            return true;
        }

        public void VoteForKeyword(Guid categoryId, string keywordText, bool correct, int weight)
        {
            var keyword = keywordProvider.Get(keywordText, categoryId);
            if (keyword == null)
            {
                keyword = new CategoryKeyword() { CategoryId = categoryId, Keyword = keywordText };
            }
            keyword.SuccessVotes += correct ? weight : 0;
            keyword.TotalVotes += weight;

            keywordProvider.Save(keyword);
        }

        public bool TryAttachToCategory(Transaction transaction)
        {
            var keywords = transaction.Keywords.Split(Constants.Keywords.Separator);

            foreach(var keyword in keywords)
            {
                var categoryKeyword = keywordProvider.Get(keyword);
                if (categoryKeyword.Count <= 0)
                    return false;

                var trustedKeyword = TrustHelper.GetTheMostTrusted(categoryKeyword);
                if (trustedKeyword != null)
                {
                    AttachToCategory(transaction.Id, trustedKeyword.CategoryId);
                    MarkTransactionAsVoted(transaction.Id);
                    return true;
                }
            }

            return false;
        }

        public void MarkTransactionAsVoted(Guid transactionId) => transactionManager.MarkAsVoted(transactionId);
    }
}
