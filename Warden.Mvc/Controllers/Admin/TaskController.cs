using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Mvc.Controllers.Admin
{
    public class TaskController : ApiController
    {
        [HttpPost]
        public ActionResult Index()
        {
            var transaction = new List<Transaction>(new[]{
                new Transaction() { Id = Guid.NewGuid(), Keywords = "оплата за грудень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "стипендія за травень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "зарплата за вер." },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "заробітня плата" }});

            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            searchManager.Index(transaction);
            return Json(true);
        }
    }
}
