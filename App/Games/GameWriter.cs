using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Games;

public class GameWriter(AppDbContext ctx) : IGameWriter
{
    public async Task AddGames(params Game[] games)
    {
        await ctx.Games.AddRangeAsync(games); 
        await ctx.SaveChangesAsync();
    }

    public async Task RemoveGames(params string[] gamesIds)
    {
        await ctx.Games.Where(x => gamesIds.Contains(x.Id)).ExecuteDeleteAsync();
    }
}
