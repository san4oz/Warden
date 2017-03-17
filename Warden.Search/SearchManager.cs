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
using Warden.Business;
using System.Configuration;
using System.Web.Hosting;

namespace Warden.Search
{
    public class SearchManager : ISearchManager
    {
        private string luceneDir = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.ConnectionStrings["IndexDirectory"].ConnectionString);
        //private string luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "../index");
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

        public List<Transaction> Search(SearchRequest request)
        {
            throw new NotImplementedException();
        }

        protected void AddToIndex(Transaction transaction, IndexWriter writer)
        {
            var searchQuery = new TermQuery(new Term("Id", transaction.Id.ToString()));
            writer.DeleteDocuments(searchQuery);
            var doc = CreateDocument(transaction);
            writer.AddDocument(doc);
        }

        protected Document CreateDocument(Transaction transaction)
        {
            var doc = new Document();
            doc.Add(new Field(Constants.Search.Id, transaction.Id.ToString(), Field.Store.YES, Field.Index.NO));

            var keywords = transaction.Keywords;
            doc.Add(new Field(Constants.Search.Keywords, keywords, Field.Store.YES, Field.Index.ANALYZED));
            return doc;
        }
    }
}
