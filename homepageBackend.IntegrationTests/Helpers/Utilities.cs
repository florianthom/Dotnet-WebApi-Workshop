using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Data;
using homepageBackend.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace homepageBackend.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static async Task InitializeDbForTests(DataContext db, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // db.Projects.Add(GetSeedingAdminUser());
            // db.SaveChanges();
            
            var administratorRole = new IdentityRole(configuration["SeedAdminProfile:IdentityRoleName"]);

            ApplicationUser administrator = new ApplicationUser()
            {
                Email = configuration["SeedAdminProfile:Email"], UserName = configuration["SeedAdminProfile:Email"]
            };
                
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }
            
            if (userManager.Users.All(u => u.Email != administrator.Email))
            {
                await userManager.CreateAsync(administrator, configuration["SeedAdminProfile:Password"]);
                await userManager.AddToRolesAsync(administrator, new[] {administratorRole.Name});
            }
        }
    }
}