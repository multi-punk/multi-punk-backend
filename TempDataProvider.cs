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
    public Dictionary<string, List<string>> PlayersInQueue { get; private set; }

    public TempDataProvider(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Games = ctx.Games.ToList();
        PlayersInQueue  = new Dictionary<string, List<string>>();
        foreach(var game in ctx.Games)
            PlayersInQueue[game.Id] = new List<string>();
    }
}
