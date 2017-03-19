using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.NLP
{

    public static class WordHelper
    {
        public static string[] Suffixes = 
             { "ик", "івник", "чик", "щик", "ив", "альник", "ильник", "ільник", "альність ",
                   "аль", "ень", "ець", "єць", "ість", "тель ", "инн", "иння", "інн", "іння", "анн", "ання", "янн", "яння", "енн", "ення", "ен", "еня"
             };

        public static string RemoveSuffix(string word)
        {
            foreach(var suffix in Suffixes)
            {
                if (word.EndsWith(suffix))
                    return word.Replace(suffix, "");
            }

            return word;
        }

        private static BestStrings LongestCommonSubstring(string s1, string s2)
        {
            var lcs = new BestStrings();

            for (var i = 1 - s2.Length; i < s1.Length; i++)
            {
                var substrings = BestSubstringWithAlignment(s1, s2, i);

                if (substrings.Length == 0) continue;

                lcs.Add(substrings);
            }

            return lcs;
        }

        private static BestStrings BestSubstringWithAlignment(string s1, string s2, int offset)
        {
            var substrings = new BestStrings();

            var substring = "";
            for (var i = Math.Max(0, offset); i < s1.Length && i < s2.Length + offset; i++)
            {
                var c1 = s1[i];

                var c2 = s2[i - offset];

                if (c1 == c2)
                {
                    substring = substring + c1;
                }
                else
                {
                    substrings.Add(substring);
                    substring = "";
                }
            }
            substrings.Add(substring);

            return substrings;
        }

        sealed class BestStrings : Collection<string>
        {
            public int Length
            {
                get { return base[0].Length; }
            }

            public BestStrings()
            {
                base.Add("");
            }

            public new void Add(string s)
            {
                if (s.Length == 0 || s.Length < Length || Contains(s)) return;

                if (s.Length > Length) Clear();
                base.Add(s);
            }

            public void Add(IEnumerable<string> collection)
            {
                foreach (var s in collection) Add(s);
            }
        }
    }
   
}
