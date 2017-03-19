using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Search.Utils.Tokenizer;
using Warden.Mvc.Helpers;
using Warden.Core.NLP;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : ApiController
    {
        private IExternalApi ExternalApi { get; set; }
        private ITransactionDataProvider TransactionProvider { get; set; }

        public TransactionController(IExternalApi externalApi, ITransactionDataProvider transactionProvider)
        {
            ExternalApi = externalApi;
            TransactionProvider = transactionProvider;
        }

        public ActionResult Index()
        {
            var transaction = new List<Transaction>(new[]{
                new Transaction() { Id = Guid.NewGuid(), Keywords = "оплата за грудень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "стипендія за травень" } });

            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            searchManager.Index(transaction);
            return View();
        }
    }
}
