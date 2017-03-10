using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Core.Readers;

namespace Warden.ExternalDataProvider
{
    public class ExternalDataProvider : IExternalDataProvider
    {
        public List<Transaction> GetTransactions(TransactionRequest request)
        {

            using (var fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "App_Data/mocked.csv"))
            using (var reader = new StreamReader(fs))
            {
                var transactions = new CsvReader().ExtractEntities<Transaction>(reader.BaseStream, list =>
                {
                    return new Transaction()
                    {
                        PayerId = list[2],
                        ReceiverId = list[6],
                        Price = decimal.Parse(list[10]),
                        Keywords = list[11],
                        Date = DateTime.Parse(list[1])
                    };
                });

                return transactions;
            }
        }
    }
}
