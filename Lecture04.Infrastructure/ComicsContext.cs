using System;
using Lecture04.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lecture04.Infrastructure
{
    public class ComicsContext : DbContext
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
        }
    }
}
