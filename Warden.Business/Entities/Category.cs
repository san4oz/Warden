using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Entities
{
    public class Category : Entity
    {
        public string Title { get; set; }

        public string Query { get; set; }
    }
}
