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

namespace App.Queue;

public class QueueService(AppDbContext ctx, IHubContext<QueueHub, IQueueHub> hub, TempDataProvider provider, IServerReader serverReader, IServerWriter serverWriter): IQueueService
{
    private Dictionary<string, List<string>> usersInGames = provider.UsersInGames;

    public async Task AddUser(string userXUId, string gameId)
    {
        if(usersInGames[gameId].Any(x => x == userXUId)) return;

        var game = ctx
            .Games
            .FirstOrDefault(x => x.Id == gameId);

        usersInGames[gameId].Add(userXUId);
        Server freeServer = await serverReader.GetFreeServer(gameId);
        Console.WriteLine(JsonSerializer.Serialize(usersInGames));

        if(usersInGames[gameId].Count >= game.MinPlayersCount && freeServer is not null) 
        {
            provider.Queues.Add(new GameQueue(game, freeServer.Id, CountDown, AfterCountDown));
            await serverWriter.ReserveServer(freeServer.Id);
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
            await serverWriter.ExemptServer(q.ServerId);
            await hub.Clients.All.StopCountdown(gameId, usersInGames[gameId]);
        }
    }

    private async void CountDown(int time)
    {
        await hub.Clients.All.Countdown($"time: {time}", ["da"]);
        Thread.Sleep(1000);
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
