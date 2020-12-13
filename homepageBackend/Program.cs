using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Data;
using homepageBackend.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace homepageBackend
{
    public class Program
    {
        // dotnet run watch
        //
        // docker build -t homepagebackend .
        // docker run -p 5000:5000 homepagebackend
        //
        // docker-compose build
        // docker-compose up
        // https://0.0.0.0:5000/swagger/index.html
        // docker-compose down
        
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                await dbContext.Database.MigrateAsync();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await DataContextSeed.SeedDefaultUserAsync(userManager, roleManager);
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}