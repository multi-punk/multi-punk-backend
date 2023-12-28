using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Servers;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Servers;

public class ServerWriter(AppDbContext ctx) : IServerWriter
{
    public async Task ExemptServer(int id)
    {
        Server server = await ctx.Servers.FindAsync(id);
        server.IsInUse = false;
    }

    public async Task ReserveServer(int id)
    {
        Server server = await ctx.Servers.FindAsync(id);
        server.IsInUse = true;
    }
}
