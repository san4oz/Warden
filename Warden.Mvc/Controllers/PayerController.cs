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
        CategoryManager categoryManager;

        public PayerController()
        {
            payerManager = IoC.Resolve<PayerManager>();
            transactionManager = IoC.Resolve<TransactionManager>();
            categoryManager = IoC.Resolve<CategoryManager>();
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
            var transactions = transactionManager.GetTransactionsByPayerId(payerId).Where(t => t.CategoryId.HasValue).ToList();
            var result = new PayerDetailsViewModel();
            result.Payer.PayerId = payer.PayerId;
            result.Payer.PayerName = payer.Name;
            //result.Payer.Region = payer.Region;
            var categoryIds = transactions.Select(v => v.CategoryId.Value).ToArray();
            var categories = categoryManager.GetByIds(categoryIds);
            var groupedTransactions = transactions
                                        .Where(t => categories.Any(c => c.Id == t.CategoryId.Value))
                                        .GroupBy(t => t.CategoryId, (key, values) => 
                                                new
                                                {
                                                    Category = categories.First(c => c.Id == key.Value).Title,
                                                    Payments = values.Select(t => t.Price).ToList()
                                                })
                                        .ToDictionary(key => key.Category, value => value.Payments);

            if (!groupedTransactions.Any())
            {
                result.Chart.IsChartAvailable = false;
                return result;
            }
            result.Chart.IsChartAvailable = true;
            result.Chart.Data = groupedTransactions;
            result.Chart.ChartType = "pie";
            return result;
        }
    }
}
