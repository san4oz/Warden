using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Managers;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Mvc.Models;

namespace Warden.Mvc.Controllers.Admin
{
    public class PayerController : Controller
    {
        private PayerManager payerManager;

        public PayerController(PayerManager payerManager)
        {
            this.payerManager = payerManager;
        }

        [HttpPost]
        public ActionResult All()
        {
            var result = payerManager.All();
            return Json(result);
        }

        [HttpPost]
        public ActionResult Save(PayerModel payer)
        {
            if (!ModelState.IsValid)
                return Json(false);

            payerManager.Save(new Payer { PayerId = payer.PayerId, Name = payer.Name });

            return Json(true);
        }
    }
}
