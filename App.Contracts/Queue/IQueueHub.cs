using Infrastructure.Database.Tables;

namespace App.Contracts.Hubs;

public interface IQueueHub
{
    Task Countdown(string gameId, int timeBeforeTransfer, IEnumerable<string> users);
    Task AwaitForServer(string gameId, IEnumerable<string> users);
    Task Transfer(Server server, IEnumerable<string> users);
    Task StopCountdown(string gameId, IEnumerable<string> users);
    Task ChangeQueue(string gameId, IEnumerable<string> users);
}
