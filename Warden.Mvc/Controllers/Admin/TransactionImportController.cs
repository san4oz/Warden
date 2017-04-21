using System.IO;
using System.Net.Mime;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Helper;
using Warden.Business.Entities;
using Warden.Business.Import;
using Warden.Business.Providers;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionImportController : Controller
    {
        private readonly ITransactionImportConfigurationDataProvider configurationDataProvider;

        public TransactionImportController()
        {
            this.configurationDataProvider = IoC.Resolve<ITransactionImportConfigurationDataProvider>();
        }

        [HttpPost]
        public ActionResult StartImport(string whoId, bool rebuild)
        {
            var result = IoC.Resolve<TransactionImportTask>().StartImport(whoId, rebuild);
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
