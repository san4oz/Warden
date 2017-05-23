using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Warden.Mvc.Models.Admin
{
    public class PostViewModel
    {
        public PostComponentViewModel[] Components { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Banner { get; set; }
    }
}
