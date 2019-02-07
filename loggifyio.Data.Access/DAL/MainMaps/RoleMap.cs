using Microsoft.EntityFrameworkCore;
using loggifyio.Data.Model;

namespace loggifyio.Data.Access.DAL
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .ToTable("Role")
                .HasKey(x => x.Id);
        }
    }
}