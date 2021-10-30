using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Core
{
    public static class Extensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default)
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken)
                                            .ConfigureAwait(false))
                results.Add(item);
            return results;
        }
    }
}
