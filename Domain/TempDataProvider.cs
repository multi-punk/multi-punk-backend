using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public class TempDataProvider
{
    public List<Game> Games { get; private set; }
    public Dictionary<string, List<string>> UsersInGames { get; private set; } = [];
    public List<GameQueue> Queues = [];

    public TempDataProvider(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Games = ctx.Games.ToList();
        foreach(var game in ctx.Games)
            UsersInGames[game.Id] = new List<string>();
    }
}