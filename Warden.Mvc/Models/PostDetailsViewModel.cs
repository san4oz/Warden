using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Mvc.Models
{
    public class PostDetailsViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public string Banner { get; set; }

        public IEnumerable<PostComponentViewModel> Components { get; set; }
    }
}
