using Lucene.Net.Analysis.Tokenattributes;
using System.IO;
using System.Text;
using Warden.Search.Utils.Tokenizer;

namespace Warden.Search.Utils
{
    public class SearchHelper
    {
        public const string WildCardSymbol = "*";

        public static string GetWildCardQuery(string query, bool matchAllKeywords)
        {
            string tokenStart = matchAllKeywords ? WildCardSymbol : string.Empty;
            return GetWildCardQuery(query, tokenStart, WildCardSymbol);
        }

        protected static string GetWildCardQuery(string query, string tokenStart, string tokenEnd)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            query = query.Trim().ToLowerInvariant();
            using (var reader = new StringReader(query))
            {
                var tokenizer = new SearchTokenizer(reader);
                var termAttribute = tokenizer.AddAttribute<ITermAttribute>();
                var builder = new StringBuilder();
                while (tokenizer.IncrementToken())
                {
                    string word = termAttribute.Term;
                    builder.Append(string.Format("{0}{1}{2} ", tokenStart, word, tokenEnd));
                }
                return builder.ToString().TrimEnd();
            }
        }
    }
}
