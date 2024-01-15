using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using App.Contracts.Statistics;
using App.Contracts.Users;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Games;

public class GameWriter(AppDbContext ctx, IUserReader userReader, IStatisticWriter statisticWriter) : IGameWriter
{
    public async Task AddGames(params Game[] games)
    {
        await ctx.Games.AddRangeAsync(games); 
        
        foreach (var game in games)
            foreach (var user in await userReader.GetAllUsers())
                await statisticWriter.CreateStatistic(new Statistic(){
                    UserId = user.Id,
                    GameId = game.Id,
                    Score = 0
                });

        await ctx.SaveChangesAsync();
    }

    public async Task RemoveGames(params string[] gamesIds)
    {
        await ctx.Games.Where(x => gamesIds.Contains(x.Id)).ExecuteDeleteAsync();
    }
}
