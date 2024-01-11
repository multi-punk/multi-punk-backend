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
    public async Task AddUser(string userXUId, string gameId)
    {
        var server = ctx
            .Servers
            .FirstOrDefault(s => s.GameId == gameId && !s.IsInUse);
        if (server == null) return;
        var game = ctx
            .Games
            .FirstOrDefault(k => k.Id == gameId);
        queue[gameId].Add(userXUId);
        if (queue[gameId].Count() == game.MinPlayersCount)
        {
            server.IsInUse = true;
            ctx.Servers.Update(server);
            await hub
                .Clients
                .All
                .StartCountdown(gameId, queue[gameId], server);
        }
        else
            await hub
                .Clients
                .All
                .ChangeQueue(gameId, queue[gameId]);
        
        await ctx.SaveChangesAsync();
    }
    public async Task RemoveUser(string userXUId, string gameId)
    {
        var server = ctx
            .Servers
            .FirstOrDefault(s => s.GameId == gameId && s.IsInUse);
        if (server == null) return;
        var game = ctx
            .Games
            .FirstOrDefault(k => k.Id == gameId);
        queue[gameId].Remove(userXUId);
        if (queue[gameId].Count() < game.MinPlayersCount)
        {
            server.IsInUse = false;
            ctx.Servers.Update(server);
            await hub
                .Clients
                .All
                .StopCountdown(gameId, queue[gameId]);
        }
        else
            await hub
                .Clients
                .All
                .ChangeQueue(gameId, queue[gameId]);
        
        await ctx.SaveChangesAsync();
    }
}
