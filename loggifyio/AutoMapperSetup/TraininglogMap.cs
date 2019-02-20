using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using loggifyio.Data.Model;
using Loggifyio.Api.Models;

namespace loggifyio.AutoMapperSetup
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
