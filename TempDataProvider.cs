using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi;

public class TempDataProvider
{
    public List<Game> Games { get; private set; }
    public Dictionary<string, List<string>> Queues { get; private set; }

    public TempDataProvider(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Games = ctx.Games.ToList();
        Queues = new Dictionary<string, List<string>>();
        foreach(var game in ctx.Games)
            Queues[game.Id] = new List<string>();
    }
}
