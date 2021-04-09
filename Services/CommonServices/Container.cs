using Microsoft.Extensions.DependencyInjection;
using Services.CommonServices.Abstraction;
using Services.CommonServices.Implementation;

namespace Services.CommonServices
{
    public static class Container
    {
        public static IServiceCollection AddCommonService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IServiceResult<>), typeof(ServiceResult<>));

            return services;
        }
    }
}
