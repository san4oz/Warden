using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Entities;
using Warden.DataProvider.DataProviders;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionImportController : Controller
    {
        private readonly ITransactionImportConfigurationDataProvider configurationDatProvider;
        private readonly ITransactionImportTask importTask;

        public TransactionImportController(ITransactionImportConfigurationDataProvider configurationDataProvider, ITransactionImportTask importTask)
        {
            this.configurationDatProvider = configurationDataProvider;
            this.importTask = importTask;
        }

        [HttpPost]
        public ActionResult StartImport(string whoId)
        {
            importTask.StartImport(whoId);
            return Json(true);
        }

        public ActionResult GetImportSettings(string payerId)
        {
            var settings = configurationDatProvider.GetForPayer(payerId);
            var model = new ImportTaskSettingsModel()
            {
                FromDate = settings.StartDate,
                ToDate = settings.EndDate,
                PayerId = settings.PayerId,
            };

            return Json(model);
        }

        public ActionResult UpdateImportSettings(ImportTaskSettingsModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var settings = configurationDatProvider.GetForPayer(model.PayerId);
            if (settings == null)
                settings = new TransactionImportTaskConfiguration() { PayerId = model.PayerId };

            settings.StartDate = model.FromDate;
            settings.EndDate = model.ToDate;

            configurationDatProvider.Update(settings);

            return Json(true);
        }
    }
}
