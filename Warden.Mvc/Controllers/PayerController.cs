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
            //var payers = payerApi.GetAvailablePayers();
            //var viewModels = payers.Select(p => new PayerViewModel()
            //{
            //    Name = p.Name,
            //    Id = p.Id
            //});
            return Json(GetMockedPayers());
        }

        [HttpPost]
        public ActionResult PayerDetails(string payerId)
        {
            if (payerId.IsEmpty())
                return HttpNotFound();

            var model = CreateMockedDetailsModel(payerId);//CreateDetailsModel(payerId);

            return Json(model);
        }

        public PayerDetailsViewModel CreateDetailsModel(string payerId)
        {
            var payer = payerApi.GetPayer(payerId);
            if (payer == null)
                return null;

            var transactions = payerApi.GetTransactions(payerId);

            var model = new PayerDetailsViewModel();
            model.Total = payerApi.CalculateTotal(transactions);
            model.HighestSpendingsCategory = payerApi.GetHighestSpendingsCategory(transactions);
            model.LowestSpendingsCategory = payerApi.GetLowestSpedningsCategory(transactions);
            model.TransactionsCount = transactions.Count();
            model.Name = payer.Name;

            var data = transactions.ToDictionary(key => key.Category, value => value.TotalPrice);
            model.Data = new ChartData(data);

            return model;
        }

        private PayerDetailsViewModel CreateMockedDetailsModel(string payerId)
        {
            var payer = GetMockedPayers().First(p => p.Id == payerId);
            var model = new PayerDetailsViewModel();
            model.Name = payer.Name;
            model.Total = 127000;
            model.HighestSpendingsCategory = "Стипендія";
            model.LowestSpendingsCategory = "Ремонт";
            model.TransactionsCount = 731;
            var data = new Dictionary<string, decimal>()
            {
                 { "Спипендія", 50000 },
                 { "Ремонт", 10000 }
            };

            model.Data = new ChartData(data);

            return model;
        }

        private List<PayerViewModel> GetMockedPayers()
        {
            return new List<PayerViewModel>()
            {
                new PayerViewModel() { Id = "1", Name = "Житомирський державний технологічний університет" },
                new PayerViewModel() { Id = "2", Name = "Житомирський державний університет імені Івана Франка" }
            };
        }
    }
}
