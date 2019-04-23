using AutoMapper;
using Loggifyio.Api.Models;
using Loggifyio.Queries.QueryModel;

namespace Loggifyio.AutoMapperSetup
{
    public class UserWithTokenMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserWithToken, UserWithTokenModel>();
        }
    }
}
