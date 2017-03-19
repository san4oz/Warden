using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers.Admin
{
    public class PayerController : ApiController
    {
        private IPayerDataProvider payerProvider;

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
        public ActionResult Save(PayerModel payer)
        {
            if (!ModelState.IsValid)
                return Json(false);

            payerProvider.Save(new Payer { PayerId = payer.PayerId, Name = payer.Name });

            return Json(true);
        }
    }
}
