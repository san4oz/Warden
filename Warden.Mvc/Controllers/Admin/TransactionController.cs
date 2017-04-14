using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities.Search;
using Warden.Mvc.Models;
using Warden.Business.Contracts.Managers;
using Warden.Business;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : Controller
    {
        private AnalysisManager analysisManager;
        private TransactionManager transactionManager;

        public TransactionController(AnalysisManager analysisManager, TransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
            this.analysisManager = analysisManager;
        }

        public ActionResult Count() => Json(transactionManager.GetTotalCount());

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return Json(null);

            var unprocessedTransactions = transactionManager.SearchForUnprocessed(keyword);
            var model = unprocessedTransactions.Select(ConvertToViewModel);

            return Json(model);
        }

        [HttpPost]
        public ActionResult KeywordsToCalibrate(Guid categoryId)
        {
            var model = transactionManager.GetTransactionsToCalibrate(categoryId).Select(t =>
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

        [HttpPost]
        public ActionResult CalibrateKeywords(KeywordsCalibrationViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            foreach(var vote in model.Votes)
            {
                analysisManager.VoteForKeyword(model.CategoryId, vote.Keyword, vote.VoteResult);
            }

            analysisManager.MarkAsVoted(model.TransactionId);

            return Json(true);
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
            var result = transactionManager.GetProcessedByCategoryId(categoryId).Select(ConvertToViewModel);
            return Json(result);
        }

        private TransactionViewModel ConvertToViewModel(Transaction transaction)
        {
            return new TransactionViewModel(transaction.Id, transaction.PayerId, transaction.Price, transaction.Keywords);
        }
    }
}
