using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warden.ExternalDataProvider.Parsers;

namespace Warden.ExternalDataProvider.Providers
{
    public abstract class ExternalDataProviderBase<T>
        where T: class, new()
    {
        protected abstract string BaseUri { get; }

        protected BaseCsvParser<T> Parser { get; set; }

        public ExternalDataProviderBase()
        {
            Parser = new BaseCsvParser<T>();
        }

        protected async Task<Stream> DownloadFileAsync(Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStreamAsync(string.Format("{0}?{1}", BaseUri, BuildGETParams(parameters)));
            }
        }

        protected async Task<IList<T>> GetEntities(Dictionary<string, string> request)
        {
            var response = await DownloadFileAsync(request);
            return  Parser.ParseEntities(response);
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
