using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Extenders;

public static class EnumerableExtension
{
    public static async Task<T> PickRandom<T>(this IQueryable<T> source)
    {
        return await source.Shuffle().FirstOrDefaultAsync();
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.Shuffle().FirstOrDefault();
    }

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}
