using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;
using Warden.Business.Entities.Search;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Warden.Business;
using System.Configuration;
using System.Web.Hosting;
using Warden.Search.Utils;
using Warden.Search.Utils.Persisters;

namespace Warden.Search
{
    public class SearchManager : ISearchManager
    {
        private string luceneDir = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.ConnectionStrings["IndexDirectory"].ConnectionString);
        private FSDirectory directoryTemp;
        private Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_30;

        private FSDirectory directory
        {
            get
            {
                if (directoryTemp == null) 
                    directoryTemp = FSDirectory.Open(new DirectoryInfo(luceneDir));
                if (IndexWriter.IsLocked(directoryTemp)) 
                    IndexWriter.Unlock(directoryTemp);

                var lockFilePath = Path.Combine(luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) 
                    File.Delete(lockFilePath);
                return directoryTemp;
            }
        }

        public void Index(Transaction transaction)
        {
            Index(new Transaction[] { transaction });
        }

        public void Index(IEnumerable<Transaction> transactions)
        {
            using (var analyzer = new StandardAnalyzer(version))
            {
                using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var transaction in transactions)
                    {
                        AddToIndex(transaction, writer);
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

            using (var searcher = new IndexSearcher(directory))
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

        protected virtual IList<Entry> CreateSearchResponse(IndexSearcher searcher, IEnumerable<ScoreDoc> hits, SearchRequest request)
        {
            var result = new List<Entry>();
            //var indexInfo = new List<string>(); 
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

        protected virtual Document GetDocument(IndexSearcher searcher, int n, IList<string> fields)
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

        public void CleanIndexEntries(Guid[] ids)
        {
            using (var analyzer = new StandardAnalyzer(version))
            {
                using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    ids.ToList().ForEach(id =>
                    {
                        var query = new TermQuery(new Term("Id", id.ToString()));
                        writer.DeleteDocuments(query);
                    });
                }
            }                 
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
