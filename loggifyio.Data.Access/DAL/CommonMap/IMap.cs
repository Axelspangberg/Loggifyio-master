using Microsoft.EntityFrameworkCore;

namespace loggifyio.Data.Access.DAL
{
    public interface IMap
    {
        void Visit(ModelBuilder modelBuilder);
    }
}