using System.Linq;
using AutoMapper;
using Loggifyio.Api.Models;
using Loggifyio.Data.Model;

namespace Loggifyio.AutoMapperSetup
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
