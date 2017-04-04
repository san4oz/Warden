using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface ICategoryDataProvider : IDataProvider<Category>
    {
        void SaveUprocessedKeywords(List<UnprocessedKeyword> keywords);
        List<UnprocessedKeyword> GetUprocessedKeywords();
        UnprocessedKeyword GetUprocessedKeyword(Guid id);
        Category GetByTitle(string title);
        void DeleteUnprocessedKeyword(Guid id);   
    }
}
