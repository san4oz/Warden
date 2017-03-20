using Lucene.Net.Analysis;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Warden.Search.Utils.Tokenizer
{
    public class SearchTokenizer : CharTokenizer
    {
        protected char[] chars = new char[] { '_' };

        public SearchTokenizer(TextReader reader)
            : base(reader)
        { }

        protected override bool IsTokenChar(char c)
        {
            return Char.IsLetterOrDigit(c) || chars.Contains(c);
        }

        protected override char Normalize(char c)
        {
            return Char.ToLower(c, CultureInfo.InvariantCulture);
        }
    }
}
