using App.Contracts.Statistics;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

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
