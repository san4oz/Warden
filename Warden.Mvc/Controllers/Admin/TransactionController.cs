using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities.Search;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : ApiController
    {
        private IExternalApi externalApi;
        private ITransactionDataProvider transactionProvider;
        private ITransactionImportTask extractionTask;
        private ISearchManager searchManager;
        private ICategoryDataProvider categoryProvider;

        public TransactionController(
            IExternalApi externalApi,
            ITransactionDataProvider transactionProvider,
            ITransactionImportTask extractionTask,
            ISearchManager searchManager,
            ICategoryDataProvider categoryProvider)
        {
            this.externalApi = externalApi;
            this.transactionProvider = transactionProvider;
            this.extractionTask = extractionTask;
            this.searchManager = searchManager;
            this.categoryProvider = categoryProvider;
        }
        
        [HttpPost]
        public ActionResult StartExtraction(string whoId)
        {
            if (string.IsNullOrEmpty(whoId))
                extractionTask.RunAll();
            else
                extractionTask.RunExect(whoId);

            return Json(true);
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            var request = new SearchRequest();
            request.Query = keyword;
            request.IsWildCardSearch = true;
            var searchResult = searchManager.Search(request);

            var transactions = transactionProvider.GetUnprocessedTransactions(searchResult.Results.Select(e => new Guid(e.Id)).ToArray());

            return Json(transactions);
        }

        [HttpPost]
        public ActionResult AttachToCategory(Guid transactionId, Guid categoryId)
        {
            var keyword = transactionProvider.Get(transactionId);
            if (keyword == null)
                return HttpNotFound();

            var category = categoryProvider.Get(categoryId);
            if (category == null)
                return HttpNotFound();

            transactionProvider.AttachToCategory(transactionId, categoryId);

            return Json(true);
        }

        [HttpPost]
        public ActionResult GetProcessedTransaction(Guid categoryId)
        {
            var transactions = transactionProvider.GetProcessedTransactions(categoryId);
            return Json(transactions);
        }
    }
}
