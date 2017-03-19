using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Mvc.Models
{
    public class PayerModel
    {
        [Required]
        public string PayerId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
