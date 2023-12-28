using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using App.Contracts.Users;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Users;

public class UserWriter(AppDbContext ctx, IGameReader gameReader) : IUserWriter
{
    public async Task CreateUser(User user)
    {
        await ctx.Users.AddAsync(user);
        foreach(var game in await gameReader.GetAllGames())
            await ctx.Statistics.AddAsync(new Statistic(){
                UserId = user.Id,
                GameId = game.Id,
                Score = 0
            });
        await ctx.SaveChangesAsync();
    }

    public async Task EditUser(User user)
    {
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }
}
