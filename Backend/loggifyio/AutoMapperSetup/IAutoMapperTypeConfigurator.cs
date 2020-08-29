using AutoMapper;

namespace Loggifyio.AutoMapperSetup
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}