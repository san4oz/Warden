using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models
{
    public class PostPreviewViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public string Banner { get; set; }
    }
}
