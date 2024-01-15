using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Statistics;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Statistics;

public class StatisticReader(AppDbContext ctx) : IStatisticReader
{
    public async Task<IEnumerable<Statistic>> GetStatisticByGameId(string gameId)
    {
        var statistic = ctx.Statistics.Where(x => x.GameId == gameId).ToList();
        return statistic;
    }

    public async Task<IEnumerable<Statistic>> GetStatisticByUserId(string userId)
    {
        var statistic = ctx.Statistics.Where(x => x.UserId == userId).ToList();
        return statistic;
    }
}
