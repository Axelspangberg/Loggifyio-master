using System;
using System.Reflection.Metadata;

namespace loggifyio.Data.Model
{
    public class Traininglog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int UserId  { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }

    }
}
