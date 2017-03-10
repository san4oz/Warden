using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.Search
{
    public class SearchRequest
    {
        public string Query { get; set; }

        public int Count { get; set; }
    }
}
