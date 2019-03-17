using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Api.Common;
using Loggifyio.Api.Models;
using Loggifyio.Queries.Processors;
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

        [Fact]
        public void GetShouldReturnById()
        {
            var trainingLog = new Traininglog{Id = _random.Next(), UserId = _currentUser.Id};
            _traininglogList.Add(trainingLog);

            var result = _query.Get(trainingLog.Id);
            result.Should().Be(trainingLog);

        }

        [Fact]
        public void GetShouldThrowExceptionIfTrainingLogOfOtherUser()
        {
            var trainingLog = new Traininglog { Id = _random.Next(), UserId = _random.Next()};
            _traininglogList.Add(trainingLog);

            Action get = () => { _query.Get(trainingLog.Id); };
            get.Should().Throw<NotFoundException>();
        }
        
        [Fact]
        public void GetShouldThrowExceptionIfItemIsNotFoundById()
        {
            var trainingLog = new Traininglog { Id = _random.Next(), UserId = _currentUser.Id };
            _traininglogList.Add(trainingLog);
 
            Action get = () =>
            {
                _query.Get(_random.Next());
            };
 
            get.Should().Throw<NotFoundException>();
        }
        [Fact]
        public void GetShouldThrowExceptionIfUserIsDeleted()
        {
            var trainingLog = new Traininglog { Id = _random.Next(), UserId = _currentUser.Id, IsDeleted = true};
            _traininglogList.Add(trainingLog);

            Action get = () => { _query.Get(trainingLog.Id); };

            get.Should().Throw<NotFoundException>();

        }
        
        [Fact]
        public async Task CreateShouldSaveNew()
        {
            var model = new CreateTrainingLogModel
            {
                Date = DateTime.Now,
                Description = _random.Next().ToString(),
                Rating = _random.Next(),
                Comment = _random.Next().ToString()
            };

            var result = await _query.Create(model);

            result.Date.Should().Be(model.Date);
            result.Description.Should().Be(model.Description);
            result.Rating.Should().Be(model.Rating);
            result.Comment.Should().Be(model.Comment);
            result.UserId.Should().Be(_currentUser.Id);

            _uow.Verify(x => x.Add(result));
            _uow.Verify(x => x.CommitAsync());
            
        }
        
        [Fact]
        public async Task UpdateShouldUpdateFields()
        {
            var user = new Traininglog{Id = _random.Next(), UserId = _currentUser.Id};
            _traininglogList.Add(user);
            
            var model = new UpdateTrainingLogModel
            {
                Date = DateTime.Now,
                Description = _random.Next().ToString(),
                Rating = _random.Next(),
                Comment = _random.Next().ToString()
            };

            var result = await _query.Update(user.Id, model);

            result.Should().Be(user);
            result.Description.Should().Be(model.Description);
            result.Comment.Should().Be(model.Comment);
            result.Date.Should().BeCloseTo(model.Date);
            result.Rating.Should().Be(model.Rating);
 
            _uow.Verify(x => x.CommitAsync());
        }
        
        [Fact]
        public void UpdateShouldThrowExceptionIfItemIsNotFound()
        {
            Action create = () =>
            {
                var result = _query.Update(_random.Next(), new UpdateTrainingLogModel()).Result;
            };
 
            create.Should().Throw<NotFoundException>();
        }
        
        [Fact]
        public async Task DeleteShouldMarkAsDeleted()
        {
            var user = new Traininglog() { Id = _random.Next(), UserId = _currentUser.Id};
            _traininglogList.Add(user);
 
            await _query.Delete(user.Id);
 
            user.IsDeleted.Should().BeTrue();
 
            _uow.Verify(x => x.CommitAsync());
        }
        
        [Fact]
        public void DeleteShouldThrowExceptionIfItemIsNotBelongTheUser()
        {
            var expense = new Traininglog { Id = _random.Next(), UserId = _random.Next() };
            _traininglogList.Add(expense);
 
            Action execute = () =>
            {
                _query.Delete(expense.Id).Wait();
            };
 
            execute.Should().Throw<NotFoundException>();
        }
 
        [Fact]
        public void DeleteShouldThrowExceptionIfItemIsNotFound()
        {
            Action execute = () =>
            {
                _query.Delete(_random.Next()).Wait();
            };
 
            execute.Should().Throw<NotFoundException>();
        }
    }
}