using AutoMapper;

namespace loggifyio.AutoMapperSetup
{
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}