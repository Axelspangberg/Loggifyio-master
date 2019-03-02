using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Loggifyio.Api.IntegrationTests.Common
{
    [CollectionDefinition("ApiCollection")]
    public class DbCollection : ICollectionFixture<ApiServer>
    {
    }
}
