using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Warden.Mvc.Models
{
    public class PayerDetailsViewModel
    {
        public PayerViewModel Payer { get; set; }

        public ChartViewModel Chart { get; set; }

        public PayerDetailsViewModel()
        {
            Payer = new PayerViewModel();
            Chart = new ChartViewModel();
        }
    }
}
