using App.Contracts.Hubs;
using App.Contracts;
using Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;
using Domain;
using Microsoft.EntityFrameworkCore;
using App.Contracts.Servers;
using Infrastructure.Database.Tables;
using System.Text.Json;
using App.Servers;
using Microsoft.Extensions.DependencyInjection;

namespace App.Queue;

public class QueueService(AppDbContext ctx, IHubContext<QueueHub, IQueueHub> hub, TempDataProvider provider, IServerWriter serverWriter, IServiceScopeFactory scopeFactory): IQueueService
{
    private Dictionary<string, List<string>> usersInGames = provider.UsersInGames;

    public async Task AddUser(string userXUId, string gameId)
    {
        if(usersInGames[gameId].Any(x => x == userXUId)) return;

        var game = ctx
            .Games
            .FirstOrDefault(x => x.Id == gameId);

        usersInGames[gameId].Add(userXUId);
        Console.WriteLine(JsonSerializer.Serialize(usersInGames));

        if(usersInGames[gameId].Count >= game.MinPlayersCount) 
        {
            provider.Queues.Add(new GameQueue(game, null, CountDown, AfterCountDown));
        }
        await hub.Clients.All.ChangeQueue(gameId, usersInGames[gameId]);
    }
    
    public async Task RemoveUser(string userXUId, string gameId)
    {
        var game = ctx
            .Games
            .FirstOrDefault(x => x.Id == gameId);

        usersInGames[gameId].Remove(userXUId);
        Console.WriteLine(JsonSerializer.Serialize(usersInGames));
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

    private async void CountDown(int time, string gameId)
    {
        using var scope = scopeFactory.CreateScope();
        var innerServerReader = scope.ServiceProvider.GetRequiredService<IServerReader>();
        var innerServerWriter = scope.ServiceProvider.GetRequiredService<IServerWriter>();
        var innerHub = scope.ServiceProvider.GetRequiredService<IHubContext<QueueHub, IQueueHub>>();
        var freeServer = await innerServerReader.GetFreeServer(gameId);
        if(freeServer is null)
        {
            await innerHub.Clients.All.Countdown($"получаю сервер", ["da"]);
            return;
        } 
        await innerServerWriter.ReserveServer(freeServer.Id);
        await innerHub.Clients.All.Countdown($"time: {time}", ["da"]);
    }
    
    private async void AfterCountDown(int usersCountToTransfer, string gameId)
    {
        var usersToTransfer = usersInGames[gameId].Take(usersCountToTransfer);
        usersInGames[gameId].RemoveAll(x => usersInGames[gameId].Take(usersCountToTransfer).Contains(x));
        var q = provider
            .Queues
            .Find(x => x.GameId == gameId);
        var server = await ctx
            .Servers
            .FirstOrDefaultAsync(x => x.Id == q.ServerId);
        await hub.Clients.All.Transfer(server , usersToTransfer);
        await hub.Clients.All.ChangeQueue(gameId, usersInGames[gameId]);
    }
}
