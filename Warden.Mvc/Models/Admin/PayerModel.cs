using System.ComponentModel.DataAnnotations;

namespace Warden.Mvc.Models.Admin
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
