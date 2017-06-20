using System.Web.Mvc;
using Warden.Business.Managers;
using Warden.Mvc.Models.Frontend;
using Warden.Core.Extensions;
using System.Linq;
using Warden.Business.Api.Payer;
using System.Collections.Generic;

namespace Warden.Mvc.Controllers
{
    public class PayerController : Controller
    {
        private readonly PayerApi payerApi;

        public PayerController(PayerApi payerApi)
        {
            this.payerApi = payerApi;
        }

        [HttpPost]
        public ActionResult All()
        {
            var payers = payerApi.GetAvailablePayers();
            var viewModels = payers.Select(p => new PayerViewModel(p));
            return Json(viewModels);
        }

        [HttpPost]
        public ActionResult PayerStatistic(string payerId)
        {
            if (payerId.IsEmpty())
                return HttpNotFound();

            var model = CreatePayerStatisticViewModel(payerId);

            return Json(model);
        }

        public PayerStatisticViewModel CreatePayerStatisticViewModel(string payerId)
        {           
            var spendings = payerApi.GetSpendings(payerId);

            var model = new PayerStatisticViewModel();
            model.Total = payerApi.CalculateTotal(spendings);
            model.HighestSpendingsCategory = payerApi.GetHighestSpendingCategory(spendings);
            model.LowestSpendingsCategory = payerApi.GetLowestSpedningCategory(spendings);
            model.TransactionsCount = payerApi.GetRegisteredTransactionsCount(payerId);

            var data = spendings.ToDictionary(t => t.Category, t => t.TotalPrice);
            model.Data = new ChartData(data);

            return model;
        }
    }
}
