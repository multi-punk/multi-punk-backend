using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Statistics;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Statistics;

public class StatisticWriter(AppDbContext ctx) : IStatisticWriter
{
    public async Task CreateStatistic(Statistic statistic)
    {
        await ctx.Statistics.AddAsync(statistic);
        await ctx.SaveChangesAsync();
    }

    public async Task EditStatistic(Statistic statistic)
    {
        ctx.Statistics.Update(statistic);
        await ctx.SaveChangesAsync();
    }
}
