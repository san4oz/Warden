﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Warden.Mvc.Controllers
{
    public class PayerController : Controller
    {
        public ActionResult Details()
        {
            return Json(true);
        }
    }
}
