using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;

namespace Warden.Mvc.Controllers
{
    public class PayerController : Controller
    {
        IPayerDataProvider payerProvider;

        public PayerController(IPayerDataProvider payerProvider)
        {
            this.payerProvider = payerProvider;
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
