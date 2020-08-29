using Loggifyio.Data.Access.DAL.CommonMap;
using Loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Data.Access.DAL.MainMaps
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