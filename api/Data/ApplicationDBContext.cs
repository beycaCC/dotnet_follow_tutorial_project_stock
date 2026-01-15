using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
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
    }
}