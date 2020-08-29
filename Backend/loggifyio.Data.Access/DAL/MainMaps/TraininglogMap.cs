using Loggifyio.Data.Access.DAL.CommonMap;
using Loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Data.Access.DAL.MainMaps
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