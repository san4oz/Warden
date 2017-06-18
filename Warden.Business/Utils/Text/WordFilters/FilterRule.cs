using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Utils.Text.WordFilters
{
    public class FilterRule
    {
        protected Func<string, bool> rule;

        public FilterRule(Func<string, bool> expression)
        {
            rule = expression;
        }

        public bool IsMatch(string word)
        {
            return rule(word);
        }
    }
}
