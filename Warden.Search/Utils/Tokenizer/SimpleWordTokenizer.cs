using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Search.Utils.Tokenizer
{
    public class SimpleWordTokenizer : BaseTokenizer
    {
        public override string[] Tokenize(string text)
        {
            var cleanedText = NoiseCleaner.Clean(text);

            return cleanedText.Split(new[] { ' ', '(', ')' }).Where(word => Filter.IsTokenValid(word)).ToArray();
        }
    }
}
