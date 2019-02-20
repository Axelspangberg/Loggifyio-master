using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using loggifyio.Data.Model;
using Loggifyio.Api.Models;

namespace loggifyio.AutoMapperSetup
{
    public class UserMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<User, UserModel>();
            map.ForMember(x => x.Roles, x => x.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToArray()));
        }
    }
}
