using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models
{
    public class CategoryModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public string Keywords { get; set; }
    }
}
