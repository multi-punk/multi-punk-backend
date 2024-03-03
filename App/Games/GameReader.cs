using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Games;

public class GameReader(AppDbContext ctx) : IGameReader
{
    public async Task<IEnumerable<Game>> GetAllGames()
        => await ctx.Games.ToListAsync();
    public async Task<Game> GetGameById(string gameId)
        => await ctx.Games.FirstOrDefaultAsync(x => x.Id == gameId);
}
