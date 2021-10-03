using System;
using Microsoft.EntityFrameworkCore;

namespace Lecture05.Entities
{
    public interface IComicsContext : IDisposable
    {
        DbSet<City> Cities { get; set; }
        DbSet<Power> Powers { get; set; }
        DbSet<Character> Characters { get; set; }
        int SaveChanges();
    }
}
