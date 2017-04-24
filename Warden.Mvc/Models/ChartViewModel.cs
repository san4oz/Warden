
using System.Collections.Generic;
using System.Dynamic;

namespace Warden.Mvc.Models
{
    public class ChartViewModel
    {
        public Dictionary<string, List<decimal>> Data { get; set; }

        public string ChartType { get; set; }
    }
}
