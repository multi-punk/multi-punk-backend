using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Servers;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Servers;

public class ServerWriter(AppDbContext ctx) : IServerWriter
{
    public async Task ExemptServer(int id)
    {
        await ctx
            .Servers
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(p => p.IsInUse, false));
    }

    public async Task ReserveServer(int id)
    {
        await ctx
            .Servers
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(p => p.IsInUse, true));
    }
}
