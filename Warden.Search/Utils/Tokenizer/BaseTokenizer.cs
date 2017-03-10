using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Search.Utils.Tokenizer.Filter;

namespace Warden.Search.Utils.Tokenizer
{
    public abstract class BaseTokenizer
    {
        protected TokenFilter Filter { get; set; }

        public BaseTokenizer()
        {
            Filter = new TokenFilter();
        }

        public abstract string[] Tokenize(string text);
    }
}
