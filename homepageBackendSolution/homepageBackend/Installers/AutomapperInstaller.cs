using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace homepageBackend.Installers
{
    public static class AutomapperInstaller
    {
        public static void InstallAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}