using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Api.Common;
using Loggifyio.Api.Models;
using Loggifyio.Security;
using Remotion.Linq.Utilities;

namespace Loggifyio.Queries
{
    public class TraininglogQueryProcessor : ITraininglogQueryProcessor
    {
        private IUnitOfWork _uow;
        private readonly ISecurityContext _securityContext;

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
            if (_securityContext.IsAdministrator) return q;
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
                throw new NotFoundException("Traininglog is not found");
            }
 
            return user;        
        }

        public async Task<Traininglog> Create(CreateTrainingLogModel model)
        {
            var trainingLog = new Traininglog
            {
                UserId = _securityContext.User.Id,
                Date = model.Date,
                Description = model.Description,
                Rating = model.Rating,
                Comment = model.Comment
                
            };
            
            _uow.Add(trainingLog);
            await _uow.CommitAsync();
            
            return trainingLog;
        }

        public async Task<Traininglog> Update(int id, UpdateTrainingLogModel model)
        {
            var traininglog = GetQuery().FirstOrDefault(x => x.Id == id);

            if (traininglog == null)
            {
                throw new NotFoundException("Traininglog is not found");
            }

            traininglog.Date = model.Date;
            traininglog.Description = model.Description;
            traininglog.Rating = model.Rating;
            traininglog.Comment = model.Comment;

            await _uow.CommitAsync();
            return traininglog;        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("Traininglog is not found");
            }

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _uow.CommitAsync();        }
    }
}