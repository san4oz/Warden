using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warden.Core.TextParsers;

namespace Warden.ExternalDataProvider.Providers
{
    public abstract class ExternalDataProviderBase<T>
        where T: class, new()
    {
        protected abstract string BaseUri { get; }

        protected CsvParser<T> Parser { get; set; }

        public ExternalDataProviderBase()
        {
            Parser = new CsvParser<T>();
        }

        protected async Task<Stream> DownloadFileAsync(Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("{0}?{1}", BaseUri, BuildGETParams(parameters));
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStreamAsync();
            }
        }

        protected async Task<IList<T>> GetEntitiesAsync(Dictionary<string, string> request)
        {
            var response = await DownloadFileAsync(request);
            return Parser.ParseEntities(response);
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
