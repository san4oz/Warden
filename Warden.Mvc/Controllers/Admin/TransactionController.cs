using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities.ExternalProvider;
using Warden.Search.Utils.Tokenizer;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : Controller
    {
        private IExternalApi ExternalApi { get; set; }
        private ITransactionDataProvider TransactionProvider { get; set; }

        public TransactionController(IExternalApi externalApi, ITransactionDataProvider transactionProvider)
        {
            ExternalApi = externalApi;
            TransactionProvider = transactionProvider;
        }

        public ActionResult List()
        {
            var transactions = ExternalApi.GetTransactions(new TransactionRequest());

            var tokenizer = new SimpleWordTokenizer();

            foreach(var transaction in transactions)
            {
                transaction.Keywords = string.Join(";", tokenizer.Tokenize(transaction.Keywords));
                TransactionProvider.Save(transaction);
            }

            return View(transactions);
        }
    }
}
