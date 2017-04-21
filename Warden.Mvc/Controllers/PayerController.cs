using System;
using System.Linq;
using System.Web.Mvc;
using Warden.Business;
using Warden.Business.Providers;

namespace Warden.Mvc.Controllers
{
    public class PayerController : Controller
    {
        IPayerDataProvider payerProvider;

        public PayerController()
        {
            this.payerProvider = IoC.Resolve<IPayerDataProvider>();
        }

        [HttpPost]
        public ActionResult All()
        {
            return Json(payerProvider.All());
        }

        [HttpPost]
        public ActionResult Details(string payerId)
        {
            if (Guid.TryParse(payerId, out Guid result))
                return Json(payerProvider.Get(result));
            return Json(false);
        }

        public ActionResult Details()
        {
            return Json(true);
        }
    }
}
