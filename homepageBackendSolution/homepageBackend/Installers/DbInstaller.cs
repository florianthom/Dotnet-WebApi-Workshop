using homepageBackend.Data;
using homepageBackend.Domain;
using homepageBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace homepageBackend.Installers
{
    public static class DbInstaller
    {
        public static void InstallDb(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("homepageBackendContextPostgre")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                // to make entityFramework function with identity
                .AddEntityFrameworkStores<DataContext>();
            

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            });

            // changed to scoped because of tracking?!
            // services.AddSingleton<IProjectService, ProjectService>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}