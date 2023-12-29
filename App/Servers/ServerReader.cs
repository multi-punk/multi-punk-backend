using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Servers;
using Infrastructure.Database;

namespace App.Servers;

public class ServerReader(AppDbContext ctx) : IServerReader
{
    public async Task GetFreeServer(string gameId)
        => ctx.Servers.First(s => s.GameId == gameId && !s.IsInUse);
}
