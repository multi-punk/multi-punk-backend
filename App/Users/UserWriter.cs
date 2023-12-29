using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using App.Contracts.Statistics;
using App.Contracts.Users;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Users;

public class UserWriter(AppDbContext ctx, IGameReader gameReader, IStatisticWriter statisticWriter) : IUserWriter
{
    public async Task CreateUser(User user)
    {
        if(ctx.Users.Any(u => u.Id == user.Id))
            return;

        await ctx.Users.AddAsync(user);
        foreach(var game in await gameReader.GetAllGames())
            await statisticWriter.CreateStatistic(
                new Statistic(){
                    UserId = user.Id,
                    GameId = game.Id,
                    Score = 0
                }
            );
        await ctx.SaveChangesAsync();
    }

    public async Task EditUser(User user)
    {
        if(!ctx.Users.Any(u => u.Id == user.Id))
            return;

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }
}
