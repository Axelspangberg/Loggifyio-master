using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Data.Access.DAL.CommonMap
{
    public interface IMap
    {
        void Visit(ModelBuilder modelBuilder);
    }
}