using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace loggifyio.Data.Access.DAL
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EfUnitOfWork(DbContext context)
        {
            _context = context;
        }
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot)
        {
            return new DbTransaction(_context.Database.BeginTransaction(isolationLevel));
        }

        public void Add<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Add(obj);        }

        public void Update<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;        
        }

        public void Remove<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Remove(obj);        
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _context.Set<T>();
            
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Attach<T>(T newUser) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(newUser);        
        }
    }
}