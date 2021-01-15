using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet5BackendProject.Installers
{
    public static class AutomapperInstaller
    {
        public static void InstallAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}