using System;
using System.ComponentModel.DataAnnotations;
using Warden.Business.Import;

namespace Warden.Mvc.Models
{
    public class ImportTaskSettingsModel
    {
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [MaxLength(25)]
        public string PayerId { get; set; }

        public ImportTaskStatus Status { get; set; }
    }
}
