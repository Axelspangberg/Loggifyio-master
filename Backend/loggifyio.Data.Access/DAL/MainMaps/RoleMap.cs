using Loggifyio.Data.Access.DAL.CommonMap;
using Loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Data.Access.DAL.MainMaps
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