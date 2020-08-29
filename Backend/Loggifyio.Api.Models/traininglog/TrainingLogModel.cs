using System;

namespace Loggifyio.Api.Models.traininglog
{
    public class TrainingLogModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
