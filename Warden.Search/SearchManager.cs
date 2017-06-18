using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Warden.Business;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;
using Warden.Business.Managers;
using Warden.Search.Utils;
using Warden.Search.Utils.Persisters;

namespace Warden.Search
{
    public class SearchManager : ISearchManager
    {
        private string luceneDir = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.ConnectionStrings["IndexDirectory"].ConnectionString);
        private Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_30;

        private readonly PayerManager payerManager;

        public SearchManager(PayerManager manager)
        {
            this.payerManager = manager;
        }

        private FSDirectory GetDirectory(string path)
        {
            var result = FSDirectory.Open(new DirectoryInfo(path));
            if (IndexWriter.IsLocked(result))
                IndexWriter.Unlock(result);

            var lockFilePath = Path.Combine(path, "write.lock");
            if (File.Exists(lockFilePath))
                File.Delete(lockFilePath);
            return result;
        }

        public void Index(Transaction transaction, bool rebuild)
        {
            Index(new Transaction[] { transaction }, rebuild);
        }

        public void Index(IEnumerable<Transaction> transactions, bool rebuild)
        {
            using (var analyzer = new StandardAnalyzer(version))
            {
                var grouped = transactions.GroupBy(t => t.PayerId, (key, values) => new { Payer = key, Transactions = values });
                foreach (var item in grouped)
                {
                    var path = Path.Combine(luceneDir, item.Payer);
                    if (rebuild && System.IO.Directory.Exists(path))
                        System.IO.Directory.Delete(path, true);

                    using (var writer = new IndexWriter(GetDirectory(path), analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                    {
                        foreach (var transaction in item.Transactions)
                        {
                            AddToIndex(transaction, writer);
                        }
                    }
                }
            }
        }

        public SearchResponse Search(SearchRequest request)
        {
            var result = new SearchResponse();
            if (string.IsNullOrEmpty(request.Query))
                return result;
            result.Results = new List<Entry>();

            var payers = payerManager.All();
            var searchers = new List<IndexSearcher>();
            foreach (var directoryPath in System.IO.Directory.GetDirectories(luceneDir))
            {
                var directoryName = Path.GetFileName(directoryPath);
                if (payers.Any(p => p.PayerId == directoryName))
                {
                    searchers.Add(new IndexSearcher(GetDirectory(directoryPath)));
                }
            }

            using (var searcher = new MultiSearcher(searchers.ToArray()))
            {
                var hitsLimit = short.MaxValue;
                using (var analyzer = new StandardAnalyzer(version))
                {
                    var query = CreateQuery(request, analyzer);
                    var hits = searcher.Search(query, hitsLimit).ScoreDocs;
                    var searchResponse = CreateSearchResponse(searcher, hits, request);
                    result.Results.AddRange(searchResponse);
                }
            }
            return result;
        }

        protected virtual IList<Entry> CreateSearchResponse(Searcher searcher, IEnumerable<ScoreDoc> hits, SearchRequest request)
        {
            var result = new List<Entry>();
            var persister = new IndexFieldPersister();

            var fields = new List<string>(new string[] { string.IsNullOrEmpty(request.SearchField) ? Constants.Search.Keywords : request.SearchField });
            fields.Add(Constants.Search.Id);
            foreach (var hit in hits)
            {
                var doc = GetDocument(searcher, hit.Doc, fields);
                var entry = new Entry() { Id = doc.Get(Constants.Search.Id), Score = hit.Score };
                entry.Fields = fields.ToDictionary(key => key, value => persister.ParseRawValue(doc.Get(value)));
                result.Add(entry);
            }
            return result;
        }

        protected virtual Document GetDocument(Searcher searcher, int n, IList<string> fields)
        {
            return searcher.Doc(n, new MapFieldSelector(fields));
        }

        protected virtual Query CreateQuery(SearchRequest request, StandardAnalyzer analyzer)
        {
            if (string.IsNullOrEmpty(request.Query))
                return null;

            var searchField = string.IsNullOrEmpty(request.SearchField) ? Constants.Search.Keywords : request.SearchField;
            var parser = new QueryParser(version, searchField, analyzer);
            InitQueryParserOperator(parser, request.MatchAllKeywords);
            if (request.IsWildCardSearch)
            {
                request.Query = GetWildCardQuery(request.Query, request.MatchAllKeywords);
                InitWildCardSearch(parser);
            }
            return parser.Parse(request.Query);
        }

        protected virtual void InitQueryParserOperator(QueryParser parser, bool matchAllKeywords)
        {
            parser.DefaultOperator = matchAllKeywords ? QueryParser.Operator.OR : QueryParser.Operator.AND;
        }

        protected virtual void InitWildCardSearch(QueryParser parser)
        {
            parser.AllowLeadingWildcard = true;
            parser.DefaultOperator = QueryParser.Operator.OR;
        }

        protected virtual string GetWildCardQuery(string query, bool matchAllKeywords)
        {
            return SearchHelper.GetWildCardQuery(query, matchAllKeywords);
        }

        protected virtual void AddToIndex(Transaction transaction, IndexWriter writer)
        {
            var searchQuery = new TermQuery(new Term("Id", transaction.Id.ToString()));
            writer.DeleteDocuments(searchQuery);
            var doc = CreateDocument(transaction);
            writer.AddDocument(doc);
        }

        protected virtual Document CreateDocument(Transaction transaction)
        {
            var doc = new Document();
            doc.Add(new Field(Constants.Search.Id, transaction.Id.ToString(), Field.Store.YES, Field.Index.NO));

            var keywords = transaction.Keywords;
            doc.Add(new Field(Constants.Search.Keywords, keywords, Field.Store.YES, Field.Index.ANALYZED));
            return doc;
        }
    }
}
