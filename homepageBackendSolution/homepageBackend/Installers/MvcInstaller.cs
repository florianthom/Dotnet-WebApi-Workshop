using Microsoft.Extensions.DependencyInjection;

namespace homepageBackend.Installers
{
    public static class MvcInstaller
    {
        public static void InstallMvc(this IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}