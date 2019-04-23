using Loggifyio.Data.Access.DAL.CommonMap;
using Loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Data.Access.DAL.MainMaps
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasKey(x => x.Id);
        }
    }
}