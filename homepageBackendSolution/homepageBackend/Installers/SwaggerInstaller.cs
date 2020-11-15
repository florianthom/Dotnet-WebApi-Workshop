using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace homepageBackend.Installers
{
    public static class SwaggerInstaller
    {
        public static void InstallSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "homepageBackend", Version = "v1"});
            });
        }
    }
}