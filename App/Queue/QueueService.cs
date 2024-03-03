using App.Contracts.Hubs;
using App.Contracts;
using Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;
using Domain;
using Microsoft.EntityFrameworkCore;
using App.Contracts.Servers;
using Microsoft.Extensions.DependencyInjection;
using App.Contracts.Games;

namespace App.Queue;

public class QueueService(
    AppDbContext ctx, 
    IHubContext<QueueHub, IQueueHub> hub, 
    TempDataProvider provider, 
    IServerWriter serverWriter, 
    IServiceScopeFactory scopeFactory, 
    IGameReader gameReader
): IQueueService
{
    private Dictionary<string, List<string>> usersInGames = provider.UsersInGames;

    public async Task AddUser(string gameId, params string[] userXUId)
    {
        userXUId = userXUId
            .Where(x => usersInGames[gameId].Contains(x))
            .ToArray();
        if(!userXUId.Any()) return;

        var game = await gameReader.GetGameById(gameId);

        usersInGames[gameId].AddRange(userXUId);
        if(usersInGames[gameId].Count >= game.MinPlayersCount) 
        {
            provider.Queues.Add(new GameQueue(game, null, AfterCountDown, CountDown));
        }
        await hub.Clients.All.ChangeQueue(gameId, usersInGames[gameId]);
    }
    
    public async Task RemoveUser(string gameId, params string[] userXUId)
    {
        var game = ctx
            .Games
            .FirstOrDefault(x => x.Id == gameId);

        usersInGames[gameId].RemoveAll(x => userXUId.Contains(x));
        await hub.Clients.All.ChangeQueue(gameId, usersInGames[gameId]);

        if(usersInGames[gameId].Count < game.MinPlayersCount) 
        {
            var q = provider
                .Queues
                .Find(x => x.GameId == gameId);
            if(q is null) return;
            q.StopQueue();
            provider.Queues.Remove(q);
            if(q.ServerId is not null)
                await serverWriter.ExemptServer((int)q.ServerId);
            await hub.Clients.All.StopCountdown(gameId, usersInGames[gameId]);
        }
    }

    public async Task RemoveUserFromAllGames(string userXUId)
    {
        foreach(var usersInGame in usersInGames)
            if(usersInGame.Value.Contains(userXUId))
                await RemoveUser(usersInGame.Key, userXUId);
    }
    
    private async Task<int> CountDown(int time, string gameId, int usersCountToTransfer, GameQueue queue)
    {
        var scope = scopeFactory.CreateScope();
        var ServiceProvider = scope.ServiceProvider;

        var innerHub = ServiceProvider.GetRequiredService<IHubContext<QueueHub, IQueueHub>>();

        var innerServerReader = ServiceProvider.GetRequiredService<IServerReader>();
        var innerServerWriter = ServiceProvider.GetRequiredService<IServerWriter>();
        
        var usersToTransfer = usersInGames[gameId].Take(usersCountToTransfer);

        if(queue.ServerId is null)
        {
            var freeServer = await innerServerReader.GetFreeServer(gameId);
            
            if(freeServer is null)
            {
                await innerHub.Clients.All.AwaitForServer(gameId, usersToTransfer);
                return time;
            }
            else
            {
                queue.ServerId = freeServer.Id;
                await innerServerWriter.ReserveServer(freeServer.Id); 
            }
        }

        await innerHub.Clients.All.Countdown(gameId, time, usersToTransfer);

        time = time - 1;

        return time;
    }
    
    private async Task AfterCountDown(int usersCountToTransfer, string gameId)
    {
        var scope = scopeFactory.CreateScope();
        var ServiceProvider = scope.ServiceProvider;

        var innerHub = ServiceProvider.GetRequiredService<IHubContext<QueueHub, IQueueHub>>();
        var innerCtx = ServiceProvider.GetRequiredService<AppDbContext>();
        var queueService = ServiceProvider.GetRequiredService<IQueueService>();

        var usersToTransfer = usersInGames[gameId].Take(usersCountToTransfer);

        var q = provider
            .Queues
            .Find(x => x.GameId == gameId);
        var server = await innerCtx
            .Servers
            .FirstOrDefaultAsync(x => x.Id == q.ServerId);

        await innerHub.Clients.All.Transfer(server , usersToTransfer);
        var usersToRemove = usersInGames[gameId].Where(x => usersInGames[gameId].Take(usersCountToTransfer).Contains(x));
        await queueService.RemoveUser(gameId, usersToRemove.ToArray());
        await innerHub.Clients.All.ChangeQueue(gameId, usersInGames[gameId]);
    }
}
