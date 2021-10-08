using System;
using Microsoft.EntityFrameworkCore;

namespace Lecture05.Entities
{
    public interface IComicsContext : IDisposable
    {
        DbSet<City> Cities { get; }
        DbSet<Power> Powers { get; }
        DbSet<Character> Characters { get; }
        int SaveChanges();
    }
}
