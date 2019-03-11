using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Models;
using Loggifyio.Integration.Tests.Common;
using Loggifyio.Integration.Tests.Helpers;
using Xunit;

namespace Loggifyio.Integration.Tests.Users
{
    [Collection("ApiCollection")]
    public class PostShould
    {
        private readonly ApiServer _server;
        private HttpClientWrapper _client;
        private Random _random;

        public PostShould(ApiServer server)
        {
            _random = new Random();
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
        }

        [Fact]
        public async Task ChangePassword()
        {
            var newUser = await new Login.PostShould(_server).RegisterNewUser();
            var newPassword = _random.Next().ToString();

            await _client.PostAsync($"api/User/{newUser.Id}/password", new ChangeUserPasswordModel
            {
                Password = newPassword
            });

            //Should be able to login
            var loginedUser = await new Login.PostShould(_server).Autheticate(newUser.Username, newPassword);
            loginedUser.User.Username.Should().Be(newUser.Username);
        }
    }
}
