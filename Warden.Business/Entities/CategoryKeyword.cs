using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class CategoryKeyword : Entity
    {
        public virtual string Keyword { get; set; }

        public virtual Guid CategoryId { get; set; }

        public virtual long SuccessVotes { get; set; }

        public virtual long TotalVotes { get; set; }

        public virtual decimal TrustRate
        {
            get
            {
                return SuccessVotes / TotalVotes;
            }
        }
    }
}
