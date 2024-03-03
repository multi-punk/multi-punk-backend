using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Statistics;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Statistics;

public class StatisticReader(AppDbContext ctx) : IStatisticReader
{
    public async Task<IEnumerable<Statistic>> GetStatisticByGameId(string gameId)
    {
        var statistic = await  ctx.Statistics.Where(x => x.GameId == gameId).ToListAsync();
        return statistic;
    }

    public async Task<IEnumerable<Statistic>> GetStatisticByUserId(string userId)
    {
        var statistic = await ctx.Statistics.Where(x => x.UserId == userId).ToListAsync();
        return statistic;
    }
}
