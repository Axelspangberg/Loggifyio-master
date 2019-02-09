using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Security;
using Moq;
using Xunit;

namespace Loggifyio.Queries.Tests
{
    public class TraininglogQueryProcessorTest
    {
        private readonly Random _random;
        private Mock<IUnitOfWork> _uow;
        private readonly List<Traininglog> _traininglogList;
        private readonly User _currentUser;
        private Mock<ISecurityContext> _securityContext;
        private readonly ITraininglogQueryProcessor _query;

        public TraininglogQueryProcessorTest()
        {
            _random = new Random();
            _uow = new Mock<IUnitOfWork>();

            _traininglogList = new List<Traininglog>();
            _uow.Setup(x => x.Query<Traininglog>()).Returns(() => _traininglogList.AsQueryable());
            
            _currentUser = new User{Id = _random.Next()};
            _securityContext = new Mock<ISecurityContext>(MockBehavior.Strict);
            _securityContext.Setup(x => x.User).Returns(_currentUser);
            _securityContext.Setup(x => x.IsAdministrator).Returns(false);
            
            _query = new TraininglogQueryProcessor(_uow.Object, _securityContext.Object);
        }
        
        // THESE TESTS ARE FOR THE ORDINARY "GET" METHOD
        
        [Fact]
        public void GetShouldReturnAll()
        {
            _traininglogList.Add(new Traininglog{UserId = _currentUser.Id});

            var result = _query.Get().ToList();
            result.Count.Should().Be(1);
        }
        [Fact]
        public void GetShouldReturnOnlyUserTrainingLog()
        {
            _traininglogList.Add(new Traininglog{UserId = _currentUser.Id});
            _traininglogList.Add(new Traininglog{UserId = _random.Next()});

            var result = _query.Get().ToList();
            result.Count().Should().Be(1);
            result[0].UserId.Should().Be(_currentUser.Id);

        }
    }
}