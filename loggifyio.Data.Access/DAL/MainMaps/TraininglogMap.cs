using loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace loggifyio.Data.Access.DAL
{
    public class TraininglogMap : IMap
    {
        public void Visit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Traininglog>()
                .ToTable("TrainingLog")
                .HasKey(x => x.Id);
        }
    }
}