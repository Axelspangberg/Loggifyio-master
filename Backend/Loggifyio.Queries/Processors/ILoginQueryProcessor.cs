using System.Threading.Tasks;
using Loggifyio.Api.Models;
using Loggifyio.Data.Model;
using Loggifyio.Queries.QueryModel;

namespace Loggifyio.Queries.Processors
{
    public interface ILoginQueryProcessor
    {
            UserWithToken Authenticate(string username, string password);
            Task<User> Register(RegisterModel model);
            Task ChangePassword(ChangeUserPasswordModel requestModel);
        }
    }
