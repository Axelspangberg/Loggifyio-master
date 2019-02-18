using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using loggifyio;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Api.Common;
using Loggifyio.Queries.Processors;
using Loggifyio.Security;
using Moq;
using Xunit;

namespace Loggifyio.Queries.Tests
{
   public class LoginQueryProcessorTests
    {
        private Mock<IUnitOfWork> _uow;
        private List<User> _userList;
        private ILoginQueryProcessor _query;
        private Random _random;
        private List<Role> _roleList;
        private Mock<ITokenBuilder> _tokenBuilder;
        private Mock<IUserQueryProcessor> _userQueryProcessor;
        private Mock<ISecurityContext> _context;

        public LoginQueryProcessorTests()
        {
            _random = new Random();
            _uow = new Mock<IUnitOfWork>();

            _userList = new List<User>();
            _uow.Setup(x => x.Query<User>()).Returns(() => _userList.AsQueryable());

            _tokenBuilder = new Mock<ITokenBuilder>(MockBehavior.Strict);
            _userQueryProcessor = new Mock<IUserQueryProcessor>();

            _context = new Mock<ISecurityContext>(MockBehavior.Strict);

            _query = new LoginQueryProcessor(_uow.Object, _tokenBuilder.Object, _userQueryProcessor.Object, _context.Object);
        }

//        [Fact]
//        public void AuthenticateShouldReturnUserAndToken()
//        {
//            var password = _random.Next().ToString();
//            var user = new User
//            {
//                Username = _random.Next().ToString(),
//                Password = password,
//                Roles = new List<UserRoles>
//                {
//                    new UserRoles{Role = new Role {Name = _random.Next().ToString()}},
//                    new UserRoles{Role = new Role {Name = _random.Next().ToString()}},
//                }
//            };
//            _userList.Add(user);
//
//            var expireTokenDate = DateTime.Now + TokenAuthOption.ExpiresSpan;
//
//            var token = _random.Next().ToString();
//            _tokenBuilder.Setup(tb => tb.Build(
//                user.Username, 
//                It.Is<string[]>(roles => roles.SequenceEqual(user.Roles.Select(x => x.Role.Name).ToArray())),
//                    It.Is<DateTime>(d => d - expireTokenDate < TimeSpan.FromSeconds(1))))
//                .Returns(token);
//
//            var result = _query.Authenticate(user.Username, password);
//
//            result.User.Should().Be(user);
//            result.Token.Should().Be(token);
//            result.ExpirationDate.Should().BeCloseTo(expireTokenDate, 1000);
//        }
//        
//        [Fact]
//        public void AuthenticateShouldThrowIfUserPasswordIsWrong()
//        {
//            var password = _random.Next().ToString();
//            var user = new User
//            {
//                Username = _random.Next().ToString(),
//                Password = password,
//            };
//            _userList.Add(user);
//
//            Action execute = () => _query.Authenticate(user.Username, _random.Next().ToString());
//
//            execute.Should().Throw<BadRequest>();
//
//        }
//        
//        [Fact]
//        public void AuthenticateShouldThrowIfUserIsDeleted()
//        {
//
//        }
//        
//        [Fact]
//        public async Task RegisterShouldCreateUserViaQuery()
//        {
//
//        }
//        
//        [Fact]
//        public void ChangePasswordShouldCallUserQueryWithCurrentUser()
//        {
//
//        }
    }
}