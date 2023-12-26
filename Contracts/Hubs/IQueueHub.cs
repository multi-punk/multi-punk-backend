using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiApi.Database.Tables;

namespace MultiApi.Contracts.Hubs;

public interface IQueueHub
{
    Task ChangeQueue(string gameId, IEnumerable<string> users);
    Task StartCountdown(string gameId, IEnumerable<string> users, Server server);
    Task StopCountdown(string gameId, IEnumerable<string> users);
}
