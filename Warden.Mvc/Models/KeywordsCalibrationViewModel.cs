using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Warden.Mvc.Models
{
    public class KeywordsCalibrationViewModel
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        public List<KeywordVote> Votes { get; set; }
    }

}
