using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Loggifyio.Api.Models;
using Loggifyio.Api.Models.Common;
using Loggifyio.Integration.Tests.Common;
using Newtonsoft.Json;
using Xunit;

namespace Loggifyio.Integration.Tests.Users
{
    [Collection("ApiCollection")]
    public class GetListShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public GetListShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        public static async Task<DataResult<UserModel>> Get(HttpClient client)
        {
            var response = await client.GetAsync($"api/User");
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<DataResult<UserModel>>(responseText);
            return items;
        }

        [Fact]
        public async Task ReturnAnyList()
        {
            var items = await Get(_client);
            items.Should().NotBeNull();
            items.Data.Should().NotBeEmpty();
        }
    }
}
