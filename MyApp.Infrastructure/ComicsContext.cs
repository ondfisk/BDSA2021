using MyApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyApp.Infrastructure
{
    public class ComicsContext : DbContext, IComicsContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<Character> Characters { get; set; }

        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Character>()
                .Property(e => e.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            modelBuilder.Entity<City>()
                        .HasIndex(s => s.Name)
                        .IsUnique();

            modelBuilder.Entity<Power>()
                        .HasIndex(p => p.Name)
                        .IsUnique();
        }
    }
}
