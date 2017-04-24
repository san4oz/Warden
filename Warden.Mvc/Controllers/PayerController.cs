using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Managers;
using Warden.Business.Providers;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers
{
    public class PayerController : Controller
    {
        PayerManager payerManager;
        TransactionManager transactionManager;

        public PayerController()
        {
            payerManager = IoC.Resolve<PayerManager>();
            transactionManager = IoC.Resolve<TransactionManager>();
        }

        [HttpPost]
        public ActionResult All()
        {
            return Json(payerManager.All());
        }

        public ActionResult Details(string payerId)
        {
            var model = CreatePayerDetailsViewModel(payerId);
            return Json(model);
        }

        protected PayerDetailsViewModel CreatePayerDetailsViewModel(string payerId)
        {
            if (string.IsNullOrEmpty(payerId))
                return new PayerDetailsViewModel();

            var payer = payerManager.Get(payerId);
            var transactions = transactionManager.GetTransactionsByPayerId(payerId);
            var result = new PayerDetailsViewModel();
            result.Payer.PayerId = payer.PayerId;
            result.Payer.PayerName = payer.Name;
            //result.Payer.Region = payer.Region;
            var groupedTransactions = transactions.GroupBy(t => t.CategoryId, //CategoryName
                                        (key, values) => new { Category = key, Payments = values.Select(t => t.Price).ToList() })
                                        .ToDictionary(key => key.Category.ToString(), value => value.Payments);

            result.Chart.Data = groupedTransactions;
            result.Chart.ChartType = "pie";
            return result;
        }
    }
}
