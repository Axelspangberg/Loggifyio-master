using System.Linq;
using System.Threading.Tasks;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Api.Common;
using Loggifyio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Loggifyio.Queries.Processors
{
    public class UserQueryProcessor : IUserQueryProcessor
    {
        private IUnitOfWork _uow;

        public UserQueryProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IQueryable<User> Get()
        {
            var query = GetQuery();
            return query;
        }

        private IQueryable<User> GetQuery()
        {
            return _uow.Query<User>().Where(user => !user.IsDeleted)
                .Include(user => user.Roles)
                .ThenInclude(user => user.Role);
        }

        public User Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }
            return user;
        }

        public Task<User> Create(CreateUserModel model)
        {
            var username = model.Username.Trim();
            if (GetQuery().Any(u => u.Username == username))
            {
                throw new BadRequest("Username is already taken");
            }
            
            var user = new User
            {
                Username = model.Username.Trim(),
                Password = model.Password.Trim(), // TODO: ADD ENCRYPTION LATER
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim()
                
            };

//            AddUserRoles(user, model.Roles);

            return null;
        }

//        private void AddUserRoles(User user, string[] modelRoles)
//        {
//                        user.Roles.Clear();
//            
//                        foreach (var roleName in roleNames)
//                        {
//                            var role = _uow.Query<Role>().FirstOrDefault(x => x.Name == roleName);
//            
//                            if (role == null)
//                            {
//                                throw new NotFoundException($"Role - {roleName} is not found");
//                            }
//            
//                            user.Roles.Add(new UserRole{User = user, Role = role});
//                        }
//                    }
//        }

        public Task<User> Update(int id, UpdateUserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task ChangePassword(int id, ChangeUserPasswordModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}