using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.Search
{
    public class SearchResponse
    {
        public List<Entry> Results { get; set; }

        public int Count
        {
            get { return Results.Count; }
        }
    }
}
