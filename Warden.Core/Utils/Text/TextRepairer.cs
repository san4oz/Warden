using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.Utils.Tokenizer
{
    public static class TextRepairer
    {
        private static Dictionary<char, char> CharacterReplacementRules = new Dictionary<char, char>()
        {
            { 'i', 'і' }
        };


        public static string ProcessWronglyEncodedSymbols(string word)
        {
            var result = word;

            foreach (var rule in CharacterReplacementRules)
                result = result.Replace(rule.Key, rule.Value);

            return result;
        }
    }
}
