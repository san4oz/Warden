using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;
using Warden.Business.Providers;
using Warden.Core.TextParsers;

namespace Warden.TransactionSource
{
    public class APITransactionProvider : IAPITransactionProvider
    {
        private const string BaseUri = "http://www.007.org.ua/api/export-transactions-with-params";

        public async Task<IList<Transaction>> GetAsync(TransactionRequest request)
        {
            var webRequest = new Dictionary<string, string>()
            {
                { "from", request.From.ToString("yyyy-MM-dd") },
                { "to", request.To.ToString("yyyy-MM-dd") },
                { "who", request.PayerId },
                { "offset", request.OffsetNumber.ToString() }
            };

            var transactionsData = await GetEntitiesAsync(webRequest);

            if (transactionsData == null)
                return new List<Transaction>();

            return transactionsData.Select(td => td.ToWardenTransaction()).ToList();
        }

        private async Task<Stream> DownloadFileAsync(Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("{0}?{1}", BaseUri, BuildGETParams(parameters));
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStreamAsync();
            }
        }

        private async Task<IList<TransactionSourceItem>> GetEntitiesAsync(Dictionary<string, string> request)
        {
            var stream = await DownloadFileAsync(request);
            var parser = new CsvParser<TransactionSourceItem>();
            return parser.ParseEntities(stream);
        }

        private string BuildGETParams(Dictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var parameter in parameters)
            {
                builder.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
                builder.Append("&");
            }

            return builder.ToString().TrimEnd(new[] { '&' });
        }
    }
}
