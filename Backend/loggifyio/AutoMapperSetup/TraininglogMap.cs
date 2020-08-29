using AutoMapper;
using Loggifyio.Api.Models.traininglog;
using Loggifyio.Data.Model;

namespace Loggifyio.AutoMapperSetup
{
    public class TraininglogMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<Traininglog, TrainingLogModel>();
            map.ForMember(x => x.Username, x => x.MapFrom(y => y.User.FirstName + " " + y.User.LastName));
        }
    }
}
