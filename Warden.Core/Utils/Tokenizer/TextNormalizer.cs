using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.Utils.Tokenizer
{
    public class TextNormalizer : BaseTokenizer
    {
        public override string[] Tokenize(string text)
        {
            var cleanedText = NoiseCleaner.Clean(text);

            return cleanedText.Split(new[] { ' ', '(', ')' })
                        .Where(word => Filter.IsTokenValid(word))
                            .Select(word => TextRepairer.ProcessWronglyEncodedSymbols(word)).ToArray();
        }
    }
}
