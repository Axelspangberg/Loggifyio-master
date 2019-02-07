using loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace loggifyio.Data.Access.DAL
{
    public class UserRolesMap : IMap
    {
        public void Visit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);        
        }
    }
}