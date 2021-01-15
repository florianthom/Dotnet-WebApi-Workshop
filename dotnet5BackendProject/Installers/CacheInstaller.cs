using AutoMapper;
using dotnet5BackendProject.Cache;
using dotnet5BackendProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace dotnet5BackendProject.Installers
{
    public static class CacheInstaller
    {
        public static void InstallCacheRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            // connectionMultiplixer needed to check for redis-database state by healthchecks
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString));
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisCacheSettings.ConnectionString;
            });

            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
        
    }
}