using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    // Inherting from DbContext allows us to interface this class with a DB
    // Now that it is public, we can access this class in our other projects
    public class OdeToFoodDbContext : DbContext
    {
        // We create this constructor so our DbContext can work with the DB. Without it, it wouldn't know where to go
        // This constructor is weird to me and I don't really get it... But basically, we pass the options to the parent class (DbContext) to handle what to do
        // with the DbContextOptions object
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {

        }
        // The DbSet type allows use to query the DB along with read, write, edit, and delete
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
