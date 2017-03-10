using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities.ExternalProvider
{
    public class TransactionRequest
    {
        public string PayerId { get; set; }

        public string ReceiverId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
