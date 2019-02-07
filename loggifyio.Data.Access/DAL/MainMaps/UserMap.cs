using loggifyio.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace loggifyio.Data.Access.DAL
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