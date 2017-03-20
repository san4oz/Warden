using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Warden.Core.Utils.Tokenizer.Filter
{
    public class TokenFilter
    {
        protected List<FilterRule> Rules = new List<FilterRule>();

        protected List<string> StopwordsDictionary = new List<string>();

        public TokenFilter()
        {
            Initialize();
        }

        public bool IsTokenValid(string token)
        {
            return !Rules.Any(r => r.IsMatch(token));
        }

        protected void Initialize()
        {
            Rules.Add(new FilterRule(IsDateTime));
            Rules.Add(new FilterRule(IsNumber));
            Rules.Add(new FilterRule(IsNoise));
            Rules.Add(new FilterRule(IsStartingShit));
            Rules.Add(new FilterRule(ContainsStopword));

            InitializeStopwords();
        }

        protected void InitializeStopwords()
        {
            this.StopwordsDictionary = new List<string>()
            {
                "грн", "дог", "оплата", "акт", "надан", "отрим", "товар", "послуг", "xxxx"
            };
        }

        protected bool IsDateTime(string word)
        {
            DateTime result;
            return DateTime.TryParse(word, out result) ? true : Regex.IsMatch(word, @"^\d+/\d+/\d+$");
        }

        protected bool IsStartingShit(string word)
        {
            return Regex.IsMatch(word, @"^(\d+;)+");
        }

        protected bool IsNumber(string word)
        {
            return Regex.IsMatch(word, @"\w*\d+\w*");
        }

        protected bool IsNoise(string word)
        {
            return word.Length <= 3;
        }

        protected bool ContainsStopword(string word)
        {
            return StopwordsDictionary.Any(sw => word.IndexOf(sw) >= 0);
        }

        private string ExtractStopwords()
        {
            return StopwordsDictionary.Aggregate((prev, next) => string.Format("{0}?{1}", prev, next));
        }
    }
}
