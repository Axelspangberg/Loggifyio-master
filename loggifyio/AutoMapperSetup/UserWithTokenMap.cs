using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loggifyio.Api.Models;
using Loggifyio.Queries.QueryModel;

namespace loggifyio.AutoMapperSetup
{
    public class UserWithTokenMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserWithToken, UserWithTokenModel>();
        }
    }
}
