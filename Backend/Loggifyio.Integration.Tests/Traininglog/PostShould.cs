using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Models;
using Loggifyio.Api.Models.traininglog;
using Loggifyio.Integration.Tests.Common;
using Loggifyio.Integration.Tests.Helpers;
using Xunit;

namespace Loggifyio.Integration.Tests.Traininglog
{
    [Collection("ApiCollection")]
    public class PostShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;
        private Random _random;

        public PostShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
            _random = new Random();
        }

        [Fact]
        public async Task<TrainingLogModel> CreateNew()
        {
            var requestItem = new CreateTrainingLogModel()
            {
                Rating = _random.Next(),
                Comment = _random.Next().ToString(),
                Date = DateTime.Now.AddMinutes(-15),
                Description = _random.Next().ToString()
            };

            var createdItem = await _client.PostAsync<TrainingLogModel>("api/Traininglog", requestItem);

            //createdItem.Id.Should().BeGreaterThan(0);
            createdItem.Rating.Should().Be(requestItem.Rating);
            createdItem.Comment.Should().Be(requestItem.Comment);
            createdItem.Date.Should().Be(requestItem.Date);
            createdItem.Description.Should().Be(requestItem.Description);
            createdItem.Username.Should().Be("admin admin");

            return createdItem;
        }
    }
}
