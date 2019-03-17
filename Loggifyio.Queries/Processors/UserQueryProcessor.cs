using System;
using System.Linq;
using System.Threading.Tasks;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using loggifyio.Encryption;
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

        public async Task<User> Create(CreateUserModel model)
        {
            var username = model.Username.Trim();
            if (GetQuery().Any(u => u.Username == username))
            {
                throw new BadRequest("Username is already taken");
            }
            
            var user = new User
            {
                Username = model.Username.Trim(),
                Password = model.Password.Trim().WithBCrypt(),
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim()
                
            };

            AddUserRoles(user, model.Roles);
            _uow.Add(user);
            await _uow.CommitAsync();

            return user;
        }

        private void AddUserRoles(User user, string[] roleNames)
        {
            user.Roles.Clear();

            foreach (var roleName in roleNames)
            {
                var role = _uow.Query<Role>().FirstOrDefault(x => x.Name == roleName);

                if (role == null)
                {
                    throw new NotFoundException($"Role - {roleName} is not found");
                }

                user.Roles.Add(new UserRoles{User = user, Role = role});
            }
        }

        public async Task<User> Update(int id, UpdateUserModel model)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            AddUserRoles(user, model.Roles);

            await _uow.CommitAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);
            
            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (user.IsDeleted) return;
            
            user.IsDeleted = true;
            await _uow.CommitAsync();

        }

        public async Task ChangePassword(int id, ChangeUserPasswordModel model)
        {
            var user = Get(id);
            user.Password = model.Password.WithBCrypt();
            await _uow.CommitAsync();
        }
    }
}