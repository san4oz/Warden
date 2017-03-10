using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Transaction : Entity
    {
        public string PayerId { get; set; }

        public string ReceiverId { get; set; }

        public decimal Price { get; set; }

        public string Keywords { get; set; }

        public DateTime Date { get; set; }
    }
}
