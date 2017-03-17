﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Search.Utils.Tokenizer;
using Warden.Mvc.Helpers;

namespace Warden.Mvc.Controllers.Admin
{
    public class TransactionController : ApiController
    {
        private IExternalDataProvider Provider { get; set; }
        private ITransactionDataProvider TransactionProvider { get; set; }

        public TransactionController(IExternalDataProvider externalProvider, ITransactionDataProvider transactionProvider)
        {
            Provider = externalProvider;
            TransactionProvider = transactionProvider;
        }

        public ActionResult List()
        {
            var transactions = Provider.GetTransactions(new TransactionRequest());

            var tokenizer = new SimpleWordTokenizer();

            foreach(var transaction in transactions)
            {
                transaction.Keywords = string.Join(";", tokenizer.Tokenize(transaction.Keywords));
                TransactionProvider.Save(transaction);
            }

            return ToJson(transactions, allowGet: true);
        }

        public ActionResult Index()
        {
            var transaction = new List<Transaction>(new[]{
                new Transaction() { Id = Guid.NewGuid(), Keywords = "оплата за грудень" },
                new Transaction() { Id = Guid.NewGuid(), Keywords = "стипендія за травень" } });

            var searchManager = DependencyResolver.Current.GetService<ISearchManager>();
            searchManager.Index(transaction);
            return View();
        }
    }
}
