using System.Threading.Tasks;
using loggifyio.Data.Model;
using Loggifyio.Api.Models;
using Loggifyio.Queries.QueryModel;

namespace Loggifyio.Queries.Processors
{
    public class LoginQueryProcessor : ILoginQueryProcessor
    {
        public UserWithToken Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Register(RegisterModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task ChangePassword(ChangeUserPasswordModel requestModel)
        {
            throw new System.NotImplementedException();
        }
    }
}