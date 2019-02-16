using System.Linq;
using System.Threading.Tasks;
using loggifyio.Data.Model;
using Loggifyio.Api.Models;

namespace Loggifyio.Queries.Processors
{
    public interface IUserQueryProcessor
    {
        IQueryable<User> Get();
        User Get(int id);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(int id, UpdateUserModel model);
        Task Delete(int id);
        Task ChangePassword(int id, ChangeUserPasswordModel model);
    }
}