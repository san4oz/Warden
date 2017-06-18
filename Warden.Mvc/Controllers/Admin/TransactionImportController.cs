using System.IO;
using System.Net.Mime;
using System.Web.Mvc;
using Warden.Business.Entities;
using Warden.Business.Import;
using Warden.Business.Providers;
using Warden.Business.Utils;
using Warden.Mvc.Models.Admin;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionImportController : Controller
    {
        private readonly IImportSettingsProvider importSettingsManager;
        private readonly TransactionImportTask importTask;

        public TransactionImportController(IImportSettingsProvider importSettingsManager, TransactionImportTask importTask)
        {
            this.importSettingsManager = importSettingsManager;
            this.importTask = importTask;
        }

        [HttpPost]
        public ActionResult StartImport(string whoId, bool rebuild)
        {
            importTask.Execute(whoId, rebuild);
            return Json(ImportTaskStatus.InProgress);
        }

        public ActionResult GetImportSettings(string payerId)
        {
            var settings = importSettingsManager.GetByPayerId(payerId);
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

            var settings = importSettingsManager.GetByPayerId(model.PayerId);
            if (settings == null)
                settings = new TransactionImportSettings() { PayerId = model.PayerId };

            settings.StartDate = model.FromDate;
            settings.EndDate = model.ToDate;

            importSettingsManager.Update(settings);

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
