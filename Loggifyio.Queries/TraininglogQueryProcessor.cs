using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Api.Common;
using Loggifyio.Api.Models;
using Loggifyio.Security;

namespace Loggifyio.Queries
{
    public class TraininglogQueryProcessor : ITraininglogQueryProcessor
    {
        private IUnitOfWork _uow;
        private ISecurityContext _securityContext;

        public TraininglogQueryProcessor(IUnitOfWork uow, ISecurityContext securityContext)
        {
            _uow = uow;
            _securityContext = securityContext;
        }
        
        public IQueryable<Traininglog> Get()
        {
            var query = GetQuery();
            return query;
        }

        private IQueryable<Traininglog> GetQuery()
        {
            var q = _uow.Query<Traininglog>().Where(x => !x.IsDeleted);
            if (_securityContext.IsAdministrator)
            {
                var userId = _securityContext.User.Id;
                q = q.Where(x => x.UserId == userId);
            }

            return q;
        }

        public Traininglog Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);
 
            if (user == null)
            {
                throw new NotFoundException("Expense is not found");
            }
 
            return user;        
        }

        public Task<Traininglog> Create(CreateTrainingLogModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Traininglog> Update(int id, UpdateTrainingLogModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}