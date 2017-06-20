using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Models
{
    public class PayerModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public PayerModel(Payer payer)
        {
            this.Id = payer.PayerId;
            this.Name = payer.Name;
            this.Logo = payer.Logo;
        }
    }
}
