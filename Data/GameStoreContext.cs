// These using directives bring in required namespaces
using Microsoft.EntityFrameworkCore;         // For EF Core functionality
using MyEmptyWebApp.Entities;                // For your custom entity classes (Game, Genre, etc.)

namespace MyEmptyWebApp.Data                 // The namespace where this context lives
{
    // This is your EF Core database context class
    // It inherits from DbContext and is configured via dependency injection
    public class GameStoreContext(DbContextOptions<GameStoreContext> options)
        : DbContext(options) // Pass the options to the base DbContext constructor
    {
        // DbSet<Game> represents the Games table in the database
        // EF will track Game objects and map them to this table
        public DbSet<Game> Games => Set<Game>();

        // DbSet<Genre> represents the Genres table in the database
        public DbSet<Genre> Genres => Set<Genre>();

        // This method lets you configure your model when EF builds it
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data into the Genres table
            // This will add 5 genres with predefined Ids and Names
            modelBuilder.Entity<Genre>().HasData(
                new { Id = 1, Name = "Fighting" },
                new { Id = 2, Name = "Roleplaying" },
                new { Id = 3, Name = "Sports" },
                new { Id = 4, Name = "Racing" },
                new { Id = 5, Name = "Kids and Family" }
            );

            // EF uses this to insert the records during a database migration
        }
    }
}
