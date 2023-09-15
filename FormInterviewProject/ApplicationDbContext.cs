using FormInterviewProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FormInterviewProject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.States)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Cascade); // Optionally, cascade delete if desired

            modelBuilder.Entity<State>()
                .HasMany(s => s.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Cascade); // Optionally, cascade delete if desired
        }
    }
}
