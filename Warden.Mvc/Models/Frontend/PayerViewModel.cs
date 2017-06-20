using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Models;

namespace Warden.Mvc.Models.Frontend
{
    public class PayerViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public PayerViewModel(PayerModel payer)
        {
            this.Id = payer.Id;
            this.Name = payer.Name;
            this.Logo = payer.Logo;
        }
    }
}
