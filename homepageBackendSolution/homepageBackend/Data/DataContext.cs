using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homepageBackend.Domain;

namespace homepageBackend.Data
{
    public class DataContext: IdentityDbContext<ApplicationUser>
    {
        // public DbSet<Book> Books { get; set; }

        
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

    }
}