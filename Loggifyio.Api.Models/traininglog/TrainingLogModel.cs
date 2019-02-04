using System;
using System.ComponentModel.DataAnnotations;

namespace Loggifyio.Api.Models
{
    public class TrainingLogModel
    {

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
