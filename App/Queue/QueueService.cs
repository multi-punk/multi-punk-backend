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
        var playerServer = ctx.Servers.FirstOrDefault(k => k.GameId == gameId);
        if (playerServer == null) return;
        var game = ctx.Games.FirstOrDefault(k => k.Id == gameId);
        if (queue[gameId].First().Length < game.MinPlayersCount)
        {
            playerServer.IsInUse = false;
            playerServer.GameId = null;
            ctx.Servers.Update(playerServer);
            await hub.Clients.All.StopCountdown(gameId, queue[gameId]);
        }
        else
        {
            await hub.Clients.All.StartCountdown(gameId, queue[gameId], playerServer);
        }
        if (queue[gameId].First().Length < game.MaxPlayersCount)
            await hub.Clients.All.ChangeQueue(gameId, queue[gameId]);
    }
}
