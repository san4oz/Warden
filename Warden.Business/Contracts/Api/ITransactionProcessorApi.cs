using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Api
{
    public interface ITransactionTextProcessor
    {
        void MakeUpKeywords(IEnumerable<Transaction> transactions);
    }
}
