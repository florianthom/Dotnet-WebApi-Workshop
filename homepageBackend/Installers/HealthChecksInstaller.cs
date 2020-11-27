using homepageBackend.Data;
using homepageBackend.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace homepageBackend.Installers
{
    public static class HealthChecksInstaller
    {
        public static void InstallHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddCheck<RedisHealthCheck>("redis");
        }
    }
}