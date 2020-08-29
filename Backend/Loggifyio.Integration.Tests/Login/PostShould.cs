using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Models;
using Loggifyio.Integration.Tests.Common;
using Loggifyio.Integration.Tests.Helpers;
using Xunit;

namespace Loggifyio.Integration.Tests.Login
{
    [Collection("ApiCollection")]
    public class PostShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;
        private Random _random;

        public PostShould(ApiServer server)
        {
            _random = new Random();
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
        }

        [Fact]
        public async Task AuthenticateAdmin()
        {
            var username = "admin";
            var password = "admin";
            var result = await Autheticate(username, password);

            result.User.Username.Should().Be(username);
        }

        public async Task<UserWithTokenModel>  Autheticate(string username, string password)
        {
            var response = await _client.PostAsync<UserWithTokenModel>("api/Login/Authenticate", new LoginModel
            {
                Username = username,
                Password = password
            });

            return response;
        }

        [Fact]
        public async Task<UserModel> RegisterNewUser()
        {
            var requestItem = new RegisterModel
            {
                Username = "TU_" + _random.Next(),
                Password = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                FirstName = _random.Next().ToString()
            };

            var createdUser = await _client.PostAsync<UserModel>("api/Login/Register", requestItem);

            createdUser.Roles.Should().BeEmpty();
            createdUser.Username.Should().Be(requestItem.Username);
            createdUser.LastName.Should().Be(requestItem.LastName);
            createdUser.FirstName.Should().Be(requestItem.FirstName);

            return createdUser;
        }

        [Fact]
        public async Task ChangeUserPassword()
        {
            var requestItem = new RegisterModel
            {
                Username = "TU_" + _random.Next(),
                Password = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                FirstName = _random.Next().ToString()
            };

            await _client.PostAsync<UserModel>("api/Login/Register", requestItem);

            var newClient = new HttpClientWrapper(_server.GetAuthenticatedClient(requestItem.Username, requestItem.Password));

            var newPassword = _random.Next().ToString();
            await newClient.PostAsync($"api/Login/password", new ChangeUserPasswordModel
            {
                Password = newPassword
            });

            await Autheticate(requestItem.Username, newPassword);
        }

    }
}
