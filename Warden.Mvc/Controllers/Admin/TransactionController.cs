using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : Controller
    {
        private IExternalDataProvider Provider;

        public TransactionController(IExternalDataProvider transactionProvider)
        {
            Provider = transactionProvider;
        }

        public ActionResult List()
        {
            var transactions = Provider.GetTransactions(new TransactionRequest());
            return View(transactions);
        }
    }
}
