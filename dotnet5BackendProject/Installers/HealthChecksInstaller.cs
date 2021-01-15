using dotnet5BackendProject.Data;
using dotnet5BackendProject.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet5BackendProject.Installers
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