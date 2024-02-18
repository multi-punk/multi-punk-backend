using App.Contracts.Servers;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Servers;

public class ServerReader(AppDbContext ctx) : IServerReader
{
    public async Task<Server> GetFreeServer(string gameId)
        => await ctx.Servers.FirstOrDefaultAsync(s => s.GameId == gameId && !s.IsInUse);
}
