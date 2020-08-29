using System.Linq;
using System.Threading.Tasks;
using Loggifyio.Api.Models;
using Loggifyio.Api.Models.traininglog;
using Loggifyio.Data.Model;

namespace Loggifyio.Queries.Processors
{
    public interface ITraininglogQueryProcessor
    {
        IQueryable<Traininglog> Get();
        Traininglog Get(int id);
        Task<Traininglog> Create(CreateTrainingLogModel model);
        Task<Traininglog> Update(int id, UpdateTrainingLogModel model);
        Task Delete(int id);
    }
}