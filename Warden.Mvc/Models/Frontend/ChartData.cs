using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models.Frontend
{
    public class ChartData
    {
        public List<string> Labels { get; set; }

        public List<decimal> Values { get; set; }

        public ChartData(Dictionary<string, decimal> data)
        {
            this.Labels = new List<string>();
            this.Values = new List<decimal>();

            foreach(var pair in data)
            {
                Labels.Add(pair.Key);
                Values.Add(pair.Value);
            }
        }
    }
}
