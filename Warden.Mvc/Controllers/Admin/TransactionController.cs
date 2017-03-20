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
        private ITransactionExtractionTask extractionTask;

        public TransactionController(
            IExternalApi externalApi,
            ITransactionDataProvider transactionProvider,
            ITransactionExtractionTask extractionTask)
        {
            this.externalApi = externalApi;
            this.transactionProvider = transactionProvider;
            this.extractionTask = extractionTask;
        }
        
        [HttpPost]
        public ActionResult Get(string whoId)
        {
            var result = transactionProvider.All().Where(t => t.PayerId.Equals(whoId));
            return Json(result);
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

        public ActionResult Index()
        {
            var transaction = new List<Transaction>(new[]{
                new Transaction() { Id = Guid.NewGuid(), Keywords = "оплата за грудень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "стипендія за травень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "зарплата за вер." },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "заробітня плата" }});

            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            searchManager.Index(transaction);
            return Json(true);
        }

        public ActionResult Search()
        {
            var request = new SearchRequest();
            request.Query = "зарплата";
            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            searchManager.Search(request);
            return Json(true);
        }
    }
}
