namespace Warden.Business.Entities.Search
{
    public class SearchRequest
    {
        public string SearchField { get; set; }

        public bool MatchAllKeywords { get; set; }

        public bool IsWildCardSearch { get; set; }

        public string Query { get; set; }

        public int Count { get; set; }
    }
}
