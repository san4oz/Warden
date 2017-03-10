using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Search.Utils.Tokenizer
{
    public static class NoiseCleaner
    {
        public static string Clean(string text)
        {
            return text.Replace('.', ' ').Replace(',', ' ').Replace(';', ' ');
        }
    }
}
