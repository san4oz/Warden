using System;
using System.Linq;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Mvc.Models.Admin;
using Warden.Core.Extensions;
using System.Collections.Generic;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : Controller
    {
        private AnalysisManager analysisManager;
        private TransactionManager transactionManager;

        public TransactionController(TransactionManager transactionManager, AnalysisManager analysisManager)
        {
            this.transactionManager = transactionManager;
            this.analysisManager = analysisManager;
        }

        public ActionResult Count() => Json(transactionManager.GetTotalCount());

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            if (!keyword.IsEmpty() && keyword.Length <= 3)
                return Json(null);

            var unprocessedTransactions = transactionManager.GetWithoutCategory(keyword);
            var model = RemoveDuplicates(unprocessedTransactions).Select(ConvertToViewModel).OrderByDescending(tvm => tvm.HasSameTransactions);

            return Json(model);
        }

        [HttpPost]
        public ActionResult KeywordsToCalibrate(Guid categoryId)
        {
            var notVotesTransactions = transactionManager.GetNotVoted(categoryId);           
            var model = RemoveDuplicates(notVotesTransactions).Select(t =>
            {
                var keywords = t.Keywords.Split(new[] { Constants.Keywords.Separator });

                return new KeywordsCalibrationViewModel()
                {
                    CategoryId = categoryId,
                    TransactionId = t.Id,
                    Votes = keywords.Select(k => new KeywordVote() { Keyword = k }).ToList()
                };
            });

            return Json(model);
        }

        private List<Transaction> RemoveDuplicates(List<Transaction> transactions)
        {
            var transactionsWithoutGroupDuplicates = transactions.Where(t => !t.GroupId.IsEmpty()).GroupBy(t => t.GroupId).Select(group => group.First());
            return transactions.Where(t => t.GroupId.IsEmpty()).Union(transactionsWithoutGroupDuplicates).ToList();
        }

        [HttpPost]
        public ActionResult CalibrateKeywords(KeywordsCalibrationViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var transaction = transactionManager.Get(model.TransactionId);
            if(!transaction.GroupId.IsEmpty())
            {
                CalibrateKeywordsForGroup(model, transaction);
                return Json(true);
            }

            foreach (var vote in model.Votes)
            {
                analysisManager.VoteForKeyword(model.CategoryId, vote.Keyword, vote.VoteResult, 1);
            }

            analysisManager.MarkTransactionAsVoted(model.TransactionId);

            return Json(true);
        }

        [HttpPost]
        public ActionResult ProcessCalibratedTransactions()
        {
            int attachedTransactionsCount = 0;
            var transactions = RemoveDuplicates(transactionManager.GetNotVoted());
            transactions.ForEach(transaction =>
            {
                attachedTransactionsCount += analysisManager.TryAttachToCategory(transaction) ? 1 : 0;
            });

            return Json(new { Count = attachedTransactionsCount });
        }

        private void CalibrateKeywordsForGroup(KeywordsCalibrationViewModel model, Transaction transaction)
        {
            var transactions = transactionManager.GetByGroupId(transaction.GroupId);
            foreach (var vote in model.Votes)
            {
                analysisManager.VoteForKeyword(model.CategoryId, vote.Keyword, vote.VoteResult, transactions.Count);
            }

            foreach(var usedTransaction in  transactions)
            {
                analysisManager.MarkTransactionAsVoted(usedTransaction.Id);
            }
        }

        [HttpPost]
        public ActionResult AttachToCategory(Guid transactionId, Guid categoryId)
        {
            var result = analysisManager.AttachToCategory(transactionId, categoryId);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetProcessedTransaction(Guid categoryId)
        {
            var result = transactionManager.GetByCategoryId(categoryId).Select(ConvertToViewModel);
            return Json(result);
        }

        private TransactionViewModel ConvertToViewModel(Transaction transaction)
        {
            return new TransactionViewModel(transaction.Id, transaction.PayerId, transaction.Price, transaction.Keywords, !transaction.GroupId.IsEmpty());
        }
    }
}
