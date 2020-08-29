using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using loggifyio.Data.Access.Constants;
using Loggifyio.Api.Models;
using Loggifyio.Integration.Tests.Common;
using Loggifyio.Integration.Tests.Helpers;
using Loggifyio.Integration.Tests.Users;
using Xunit;

namespace Loggifyio.Integration.Tests.Users
{
    [Collection("ApiCollection")]
    public class PutShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;
        private Random _random;

        public PutShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
            _random = new Random();
        }

        [Fact]
        public async Task UpdateExistingItem()
        {
            var item = await new Login.PostShould(_server).RegisterNewUser();

            var requestItem = new UpdateUserModel
            {
                Username = "TU_Update_" + _random.Next(),
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                Roles = new[] { Roles.Administrator }
            };

            await _client.PutAsync<UserModel>($"api/User/{item.Id}", requestItem);

            var updatedUser = await GetItemShould.GetById(_client.Client, item.Id);

            updatedUser.FirstName.Should().Be(requestItem.FirstName);
            updatedUser.LastName.Should().Be(requestItem.LastName);

            updatedUser.Roles.Should().HaveCount(1);
            updatedUser.Roles.Should().Contain(Roles.Administrator);
        }
    }
}

