using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lecture07.Infrastructure
{
    public interface IComicsContext : IDisposable
    {
        DbSet<City> Cities { get; }
        DbSet<Power> Powers { get; }
        DbSet<Character> Characters { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
