using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.DependencyInjection
{
    public static class ApplicationCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<GetAvisosRequest>());

            return services;
        }
    }
}
