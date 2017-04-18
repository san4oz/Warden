using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business
{
    public class Constants
    {
        public class Search
        {
            public const string Id = "_id";
            public const string Keywords = "_keywords";
        }

        public class Keywords
        {
            public const char Separator = ';';

            public const decimal ValidTrustRate = 0.8M;
        }
    }
}
