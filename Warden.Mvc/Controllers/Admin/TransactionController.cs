using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities.ExternalProvider;
using Warden.Search.Utils.Tokenizer;
using Warden.Mvc.Helpers;
using Warden.Core.NLP;

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
            var path = @"C:\Users\o.kotvytskyi\Documents\Visual Studio 2015\Projects\Warden-Git\Warden\Warden.Web\App_Data\ukrainian.dic";

            var transactions = Provider.GetTransactions(new TransactionRequest());

            var tokenizer = new SimpleWordTokenizer();
            var stemmer = new RussianStemmer();
            var dictionary = System.IO.File.ReadAllLines(path).ToList();
           
            foreach (var transaction in transactions)
            {
                transaction.Keywords = string.Join(";", tokenizer.Tokenize(transaction.Keywords)
                                         .Select(w =>
                                         {
                                             stemmer.Stem(w);
                                             return WordHelper.RemoveSuffix(w);
                                         }));



                TransactionProvider.Save(transaction);
            }

            return ToJson(transactions, allowGet: true);
        }
    }
}
