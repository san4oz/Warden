using System.Collections.Generic;

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
