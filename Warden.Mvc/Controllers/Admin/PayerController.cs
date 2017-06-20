using System.Web.Mvc;
using Warden.Business.Entities;
using Warden.Business.Managers;
using Warden.Mvc.Models.Admin;
using Warden.Core.Extensions;

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
        public ActionResult Get(string payerId)
        {
            if (payerId.IsEmpty())
                return Json(null);

            var payer = payerManager.Get(payerId);
            if (payer == null)
                return Json(null);

            return Json(payer);
        }

        [HttpPost]
        public ActionResult Save(PayerModel payer)
        {
            if (!ModelState.IsValid)
                return Json(false);

            payerManager.Save(new Payer { PayerId = payer.PayerId, Name = payer.Name, Logo = payer.Logo });

            return Json(true);
        }

        [HttpPost]
        public ActionResult Edit(PayerModel payer, string payerId)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var existingPayer = payerManager.Get(payerId);
            {
                existingPayer.Name = payer.Name;
                existingPayer.Logo = payer.Logo;
                existingPayer.PayerId = payer.PayerId;
            }

            payerManager.Update(existingPayer);

            return Json(true);
        }
    }
}
