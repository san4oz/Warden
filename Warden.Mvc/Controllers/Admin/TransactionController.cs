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

        public TransactionController(
            IExternalApi externalApi,
            ITransactionDataProvider transactionProvider,
            ITransactionImportTask extractionTask)
        {
            this.externalApi = externalApi;
            this.transactionProvider = transactionProvider;
            this.extractionTask = extractionTask;
        }
        
        [HttpPost]
        public ActionResult Get(string whoId)
        {
            var result = transactionProvider.All().Where(t => t.PayerId.Equals(whoId));
            return Json(result.Take(100));
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
        public ActionResult Search()
        {
            var request = new SearchRequest();
            request.Query = "зар";
            request.IsWildCardSearch = true;
            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            var result = searchManager.Search(request);
            return Json(true);
        }
    }
}
