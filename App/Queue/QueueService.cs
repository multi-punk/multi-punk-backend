using App.Contracts.Hubs;
using App.Contracts;
using Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;

namespace App.Queue;

public class QueueService(AppDbContext ctx, IHubContext<QueueHub, IQueueHub> hub): IQueueService
{
    public async Task ChangeQueue(string userXUId, string gameId)
    {
        await hub.Clients.All.ChangeQueue("test", ["test"]);
    }
}
