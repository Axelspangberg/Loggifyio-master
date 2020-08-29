using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Loggifyio.Integration.Tests.Common;
using Xunit;

namespace Loggifyio.Integration.Tests.Users
{
    [Collection("ApiCollection")]
    public class DeleteShould
    {
        private readonly ApiServer _server;
        private HttpClient _client;

        public DeleteShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        [Fact]
        public async Task DeleteExistingItem()
        {
            var item = await new Login.PostShould(_server).RegisterNewUser();

            var response = await _client.DeleteAsync(new Uri($"api/User/{item.Id}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
        }

    }
}
