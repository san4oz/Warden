using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Core.Utils.Text.WordFilters;

namespace Warden.Core.Utils.Tokenizer
{
    public class TextNormalizer
    {
        protected TokenFilter Filter = new TokenFilter();

        public string Normalize(string text)
        {
            var words = ExtractWords(RemoveNoizyCharacters(text));
                               
            var processedWords = words
                .Where(word => Filter.IsTokenValid(word))
                    .Select(word => TextRepairer.ProcessWronglyEncodedSymbols(word)).ToArray();

            return string.Join(";", processedWords);
        }

        protected string RemoveNoizyCharacters(string text)
        {
            return text.Replace('.', ' ').Replace(',', ' ').Replace(';', ' ').ToLowerInvariant();
        }

        protected string[] ExtractWords(string text)
        {
            return text.Split(new[] { ' ', '(', ')' });
        }
    }
}
