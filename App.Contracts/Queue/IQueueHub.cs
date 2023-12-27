using Infrastructure.Database.Tables;

namespace App.Contracts.Hubs;

public interface IQueueHub
{
    Task ChangeQueue(string gameId, IEnumerable<string> users);
    Task StartCountdown(string gameId, IEnumerable<string> users, Server server);
    Task StopCountdown(string gameId, IEnumerable<string> users);
}
