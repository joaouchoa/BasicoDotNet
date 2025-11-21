using Bernhoeft.GRT.Teste.Application.DependencyInjection;
using Bernhoeft.GRT.Teste.Infra.Data.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Api.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddInfraServices(configuration);

            return services;
        }
    }
}
