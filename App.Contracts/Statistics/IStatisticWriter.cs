using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Statistics;

public interface IStatisticWriter
{
    Task EditStatistic(Statistic statistic);
    Task CreateStatistic(Statistic statistic);
}
