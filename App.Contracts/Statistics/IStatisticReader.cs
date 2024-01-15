using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Statistics;

public interface IStatisticReader
{
    Task<IEnumerable<Statistic>> GetStatisticByGameId(string gameId);
    Task<IEnumerable<Statistic>> GetStatisticByUserId(string userId);
}
