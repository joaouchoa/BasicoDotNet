using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Bernhoeft.GRT.Teste.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Data.DependencyInjection
{
    public static class InfraCollectionExtensions
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AvisoDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAvisoRepository, AvisoRepository>();

            return services;
        }
    }
}