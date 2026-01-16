using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // A giant class going to allow you to search your individual tables

// ******* ASP.NET uses "Entity Framework" to convert 'db tables' --> 'objects' *******
// e.g. to represent more than one row in the db table, we can use "list".
    public class ApplicationDBContext : IdentityDbContext<AppUser> // originally: DBContext, if adding user authentication, refactor to AppDbContext
    {
        // Constructor ("ctor"+tab)
        // base() = DbContext() here, just not allowed us to put DbContext() here directly,
        // base() allows us to put DbContext to DbContext
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        // Need to manupulating the whole entire tables
        public DbSet<Stock> Stock { get; set; } // this will actually create the database for us
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Making the connections between join table (portfolio) with stock and user
            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);


            // Defining the roles.
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",    // Need capitalize for normalized name
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}