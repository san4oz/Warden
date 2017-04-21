using System.Collections.Generic;
using System.Linq;
using Warden.Business.Entities;

namespace Warden.Business.Helpers
{
    public static class TrustHelper
    {
        private static bool ShouldTrustKeyword(CategoryKeyword keyword) => keyword.TrustRate >= Constants.Keywords.ValidTrustRate;    
        
        public static CategoryKeyword GetTheMostTrusted(List<CategoryKeyword> keywords)
        {
            return keywords.Where(k => ShouldTrustKeyword(k)).OrderByDescending(x => x.TrustRate).First();
        }
    }
}
