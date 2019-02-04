using System;
using System.ComponentModel.DataAnnotations;

namespace Loggifyio.Api.Models
{
    public class CreateTrainingLogModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
