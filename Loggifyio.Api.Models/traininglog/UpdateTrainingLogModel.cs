using System;
using System.ComponentModel.DataAnnotations;

namespace Loggifyio.Api.Models
{
    public class UpdateTrainingLogModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
