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

        private static Dictionary<char, char> CharacterReplacementRules = new Dictionary<char, char>()
        {
            { 'i', 'і' }
        };

        public string Normalize(string text)
        {
            var words = ExtractWords(RemoveNoizyCharacters(text));
                               
            var processedWords = words
                .Where(word => Filter.IsTokenValid(word))
                    .Select(word => ProcessWronglyEncodedSymbols(word)).ToArray();

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

        public static string ProcessWronglyEncodedSymbols(string word)
        {
            var result = word;

            foreach (var rule in CharacterReplacementRules)
                result = result.Replace(rule.Key, rule.Value);

            return result;
        }
    }
}
