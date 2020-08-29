using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Common;
using Loggifyio.Api.Common.Exceptions;
using Loggifyio.Api.Models;
using Loggifyio.Data.Access.DAL;
using Loggifyio.Data.Model;
using Loggifyio.Queries.Processors;
using Loggifyio.Security;
using Moq;
using Xunit;

namespace Loggifyio.Queries.Tests
{
    public class UserQueryProcessorTest
    {
        private Random _random;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly List<User> _userList;
        private readonly List<Role> _roleList;
        private IUserQueryProcessor _query;


        public UserQueryProcessorTest()
        {
            _random = new Random();
            _uow = new Mock<IUnitOfWork>();

            _userList = new List<User>();
            _uow.Setup(x => x.Query<User>()).Returns(() => _userList.AsQueryable());

            _roleList = new List<Role>();
            _uow.Setup(x => x.Query<Role>()).Returns(() => _roleList.AsQueryable());

            _query = new UserQueryProcessor(_uow.Object);
        }

        // THESE TESTS COVER "GET".
        
        [Fact]
        public void GetShouldReturnAll()
        {
            _userList.Add(new User());
            var result = _query.Get();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnAllExceptDeleted()
        {
            _userList.Add(new User{IsDeleted = true});
            _userList.Add(new User());
            var result = _query.Get();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnUserById()
        {
            var user = new User{Id = _random.Next()};
            _userList.Add(user);

            var result = _query.Get(user.Id);

            result.Should().Be(user);
        }

        [Fact]
        public void GetShouldThrowExceptionIfUserIsNotFoundById()
        {
            var user = new User { Id = _random.Next() };
            _userList.Add(user);

            Action get = () =>
            {
                _query.Get(_random.Next());
            };

            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetShouldThrowExceptionIfUserIsDeleted()
        {
            var user = new User{Id = _random.Next(), IsDeleted = true};
            _userList.Add(user);

            Action get = () => { _query.Get(user.Id); };

            get.Should().Throw<NotFoundException>();
        }

        // THESE TESTS COVER "CREATE".

        [Fact]
        public async Task CreateShouldSaveNewUser()
        {
            var model = new CreateUserModel
            {
                Password = _random.Next().ToString(),
                Username = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
            };

            var result = await _query.Create(model);

            result.Password.Should().NotBeEmpty();
            result.Username.Should().Be(model.Username);
            result.LastName.Should().Be(model.LastName);
            result.FirstName.Should().Be(model.FirstName);

            _uow.Verify(x => x.Add(result));
            _uow.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task CreateShouldAddUserRoles()
        {
            var role = new Role
            {
                Name = _random.Next().ToString()
            };
            _roleList.Add(role);

            var model = new CreateUserModel
            {
            Username = _random.Next().ToString(),
            Password =_random.Next().ToString(),
            FirstName =_random.Next().ToString(),
            LastName =_random.Next().ToString(),
            Roles = new[] {role.Name}
            };

            var result = await _query.Create(model);

            result.Roles.Should().HaveCount(1);
            result.Roles.Should().Contain(x => x.User == result && x.Role == role);

        }

        [Fact]
        public void CreateShouldThrowExceptionIfUsernameIsNotUnique()
        {
            var model = new CreateUserModel
            {
                Username = _random.Next().ToString(),
            };
            
            _userList.Add(new User{Username = model.Username});

            Action create = () =>
            {
                var x = _query.Create(model).Result;
            };

            create.Should().Throw<BadRequest>();
        }

        // TEST FOR UPDATE STARTS HERE
        
        [Fact]
        public async Task UpdateShouldUpdateUserFields()
        {
            var user = new User{Id = _random.Next()};
            _userList.Add(user);
            
            var model = new UpdateUserModel
            {
                Username = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
            };

            var result = await _query.Update(user.Id, model);
            
            result.Should().Be(user);
            result.Username.Should().Be(model.Username);
            result.LastName.Should().Be(model.LastName);
            result.FirstName.Should().Be(model.FirstName);

            _uow.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task UpdateShouldUpdateRoles()
        {
            var role = new Role
            {
                Name = _random.Next().ToString()
            };
            _roleList.Add(role);

            var user = new User
            {
                Id = _random.Next(),
                Roles = new List<UserRoles>
                {
                    new UserRoles()
                }
            };
            _userList.Add(user);

            var model = new UpdateUserModel
            {
                LastName = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
                Roles = new[] { role.Name }
            };

            var result = await _query.Update(user.Id, model);

            result.Roles.Should().HaveCount(1);
            result.Roles.Should().Contain(x => x.User == result && x.Role == role);

        }

        [Fact]
        public void UpdateShouldThrowExceptionIfUserIsNotFound()
        {
            Action create = () =>
            {
                var result = _query.Update(_random.Next(), new UpdateUserModel()).Result;
            };

            create.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task DeleteShouldMarkUserAsDeleted()
        {
            var user = new User
            {
                Id = _random.Next(),
            };
            _userList.Add(user);

            await _query.Delete(user.Id);
            
            user.IsDeleted.Should().BeTrue();

            _uow.Verify(x => x.CommitAsync());
        }
        

        [Fact]
        public void DeleteShouldThrowExceptionIfUserIsNotFound()
        {
            Action create = () =>
            {
                _query.Delete(_random.Next()).Wait();
            };

            create.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task ChangePasswordShouldChangeUsersPassword()
        {
            var user = new User { Id = _random.Next() };
            _userList.Add(user);

            var newPassword = _random.Next().ToString();

            await _query.ChangePassword(user.Id, new ChangeUserPasswordModel
            {
                Password = newPassword
            });

            user.Password.Should().NotBeEmpty();

            _uow.Verify(x => x.CommitAsync());
        }
    }
}
