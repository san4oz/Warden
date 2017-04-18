using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Contracts.Scheduler;
using Warden.Business.Core;
using Warden.Business.Entities;
using Warden.DataProvider.DataProviders;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionImportController : Controller
    {
        private readonly ITransactionImportConfigurationDataProvider configurationDataProvider;
        private readonly ITransactionImportTask importTask;

        public TransactionImportController(ITransactionImportConfigurationDataProvider configurationDataProvider, ITransactionImportTask importTask)
        {
            this.configurationDataProvider = configurationDataProvider;
            this.importTask = importTask;
        }

        [HttpPost]
        public ActionResult StartImport(string whoId, bool rebuild)
        {
            var result = importTask.StartImport(whoId, rebuild);
            return Json(result);
        }

        public ActionResult GetImportSettings(string payerId)
        {
            var settings = configurationDataProvider.GetForPayer(payerId);
            var model = new ImportTaskSettingsModel()
            {
                FromDate = settings.StartDate,
                ToDate = settings.EndDate,
                PayerId = settings.PayerId,
                Status = settings.Status
            };

            return Json(model);
        }

        public ActionResult UpdateImportSettings(ImportTaskSettingsModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var settings = configurationDataProvider.GetForPayer(model.PayerId);
            if (settings == null)
                settings = new TransactionImportTaskConfiguration() { PayerId = model.PayerId };

            settings.StartDate = model.FromDate;
            settings.EndDate = model.ToDate;

            configurationDataProvider.Update(settings);

            return Json(true);
        }

        [HttpGet]
        public ActionResult ImportTaskLogs(string payerId)
        {
            if (string.IsNullOrEmpty(payerId))
                return Content("Import task wasn't fired.");

            var tracerFileName = TransactionImportTracer.GetTracerFileName(payerId);

            if (!System.IO.File.Exists(tracerFileName))
                return Content("Import task wasn't fired.");

            var file = new FileStream(tracerFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (file == null)
                return HttpNotFound();

            var contentDisposition = new ContentDisposition()
            {
                FileName = tracerFileName,
                Inline = true,
            };
            Response.AddHeader("Refresh", "5");
            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(file, "text/plain");
        }
    }
}
