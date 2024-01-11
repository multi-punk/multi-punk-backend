using App.Contracts.Hubs;
using App.Contracts;
using Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;
using App.Contracts.Types;

namespace App.Queue;



public class QueueService(AppDbContext ctx, IHubContext<QueueHub, IQueueHub> hub, TempDataProvider provider): IQueueService
{
    private Dictionary<string, List<string>> queue = provider.Queues;
    public async Task ChangeQueue(string userXUId, string gameId, ChangeQueueType type)
    {
        switch (type)
        {
            case ChangeQueueType.AddUser:
                await hub.Clients.All.ChangeQueue(gameId, queue[gameId]);
                break;
            case ChangeQueueType.RemoveUser:
                await hub.Clients.All.ChangeQueue(gameId, queue[gameId]);
                break;
            default: break;
        }
        await hub.Clients.All.ChangeQueue(gameId, queue[gameId]);
    }
}
