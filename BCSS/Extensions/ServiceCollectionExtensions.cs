using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BCSS.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBcss(this IServiceCollection services)
        {
            services.TryAddScoped<BcssService>();
            return services;
        }
    }
}
