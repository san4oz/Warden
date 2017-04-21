using System;
using System.ComponentModel.DataAnnotations;

namespace Warden.Mvc.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public string Keywords { get; set; }
    }
}
