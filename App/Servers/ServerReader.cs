using App.Contracts.Servers;
using App.Extenders;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Servers;

public class ServerReader(AppDbContext ctx) : IServerReader
{
    public async Task<Server> GetFreeServer(string gameId)
        => await ctx.Servers.Where(s => s.GameId == gameId && !s.IsInUse).PickRandom();
}
