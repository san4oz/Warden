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
    public class TransactionController : Controller
    {
        private IExternalApi externalApi;
        private ITransactionDataProvider transactionProvider;
        private ITransactionImportTask importTask;
        private ISearchManager searchManager;
        private ICategoryDataProvider categoryProvider;

        public TransactionController(
            IExternalApi externalApi,
            ITransactionDataProvider transactionProvider,
            ITransactionImportTask importTask,
            ISearchManager searchManager,
            ICategoryDataProvider categoryProvider)
        {
            this.externalApi = externalApi;
            this.transactionProvider = transactionProvider;
            this.importTask = importTask;
            this.searchManager = searchManager;
            this.categoryProvider = categoryProvider;
        }
        
        [HttpPost]
        public ActionResult StartImport(string whoId)
        {
            if (string.IsNullOrEmpty(whoId))
                importTask.RunAll();
            else
                importTask.RunExect(whoId);

            return Json(true);
        }

        public ActionResult Count()
        {
            return Json(transactionProvider.GetGeneralTransactionCount());
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            var searchResult = searchManager.Search(new SearchRequest() { Query = keyword, IsWildCardSearch = true });            
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
