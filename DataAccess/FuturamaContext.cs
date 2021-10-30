using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class FuturamaContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public FuturamaContext(DbContextOptions<FuturamaContext> options) : base(options) { }
    }
}
