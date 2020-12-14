using System;
using System.Linq;
using homepageBackend.Data;
using homepageBackend.Domain;
using homepageBackend.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace homepageBackend.IntegrationTests
{
    public class InMemoryWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        // private readonly IConfiguration _configuration;

        public InMemoryWebApplicationFactory()
        {
            
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // is like Startup-ConfigureServices-Method
            // this builder.ConfigureServices-Method is called AFTER the startups-configureServices-Method
            // because of this we can replace the apps database context here (e.g. with an inmemory one)
            builder.ConfigureServices(async services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<DataContext>));

                services.Remove(descriptor);

                services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });

                var sp = services.BuildServiceProvider();

                // access in-memory database
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<InMemoryWebApplicationFactory<TStartup>>>();
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();
                    
                    db.Database.EnsureCreated();

                    try
                    {
                        // optionally seed database here
                        await Utilities.InitializeDbForTests(db, userManager, roleManager);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}