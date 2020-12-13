using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Domain;
using Microsoft.AspNetCore.Identity;

namespace homepageBackend.Data
{
    public static class DataContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser
                {UserName = "administrator@localhost", Email = "administrator@localhost"};

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] {administratorRole.Name});
            }
        }

        public static async Task SeedSampleDataAsync(DataContext context)
        {
            // Seed, if necessary
            // if (!context.TodoLists.Any())
            // {
            //     context.TodoLists.Add(new TodoList
            //     {
            //         Title = "Shopping",
            //         Colour = Colour.Blue,
            //         Items =
            //         {
            //             new TodoItem { Title = "Apples", Done = true },
            //         }
            //     });
            //
            //     await context.SaveChangesAsync();
            // }
        }
    }
}