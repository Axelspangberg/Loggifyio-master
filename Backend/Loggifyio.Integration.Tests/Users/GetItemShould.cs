using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Models;
using Loggifyio.Integration.Tests.Common;
using Newtonsoft.Json;
using Xunit;

namespace Loggifyio.Integration.Tests.Users
{
    [Collection("ApiCollection")]
    public class GetItemShould
    {
        private readonly ApiServer _server;
        private HttpClient _client;

        public GetItemShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        [Fact]
        public async Task ReturnItemById()
        {
            var item = await new Login.PostShould(_server).RegisterNewUser();

            var result = await GetById(_client, item.Id);

            result.Should().NotBeNull();

        }
        public static async Task<UserModel> GetById(HttpClient client, int id)
        {
            var response = await client.GetAsync(new Uri($"api/User/{id}", UriKind.Relative));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserModel>(result);
        }

        [Fact]
        public async Task ShouldReturn404StatusIfNotFound()
        {
            var response = await _client.GetAsync(new Uri($"api/User/-1", UriKind.Relative));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
