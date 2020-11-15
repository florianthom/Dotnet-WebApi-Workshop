using homepageBackend.Data;
using homepageBackend.Domain;
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                // to make entityFramework function with identity
                .AddEntityFrameworkStores<DataContext>();

            services.AddAuthorization(options =>
            {
                // options.AddPolicy("EditRolePolicy",
                //     policy => policy.RequireAssertion(context =>
                //     context.User.IsInRole("Admin")
                //     &&
                //     context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true")
                //     ||
                //     context.User.IsInRole("Super Admin")
                //     ));
                // options.AddPolicy("AdminRolePolicy",
                //     policy => policy.RequireRole("Admin"));
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            });
        }
    }
}