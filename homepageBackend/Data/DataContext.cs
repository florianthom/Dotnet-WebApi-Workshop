using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Data
{
    //  dotnet ef migrations add Initial --context homepageBackend.Data.DataContext -o .\Data\Migrations
    //    - location of context and migration folder is now selected for future migration too
    //  dotnet ef database update
    //    - localhost:5432
    //    - user: postgres (default)
    //    - pw: Florian1234
    //  dotnet ef migrations add "Added_UserId_InProjects"
    //  dotnet ef database update
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProjectTag> ProjectTags { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        
        // needed to make PostTag have no id (otherwise: ef-error)
        //    - reason: its a intermediate table
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ProjectTag>().Ignore(a => a.Project).HasKey(b => new {b.ProjectId, b.TagName});
        }
    }
}