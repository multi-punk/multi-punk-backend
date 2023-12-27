using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using App.Contracts.Hubs;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace Api.Controllers.Hubs;

[AllowAnonymous]
public sealed class QueueHub: Hub<IQueueHub>
{
    private Dictionary<string, List<string>> queues;
    private AppDbContext ctx;

    public QueueHub(TempDataProvider dataProvider, AppDbContext ctx)
    {
        queues = dataProvider.Queues;
        this.ctx = ctx;
    }

    public override async Task OnConnectedAsync()
    {
        foreach(var game in ctx.Games)
            await Clients.Caller.ChangeQueue(game.Id, queues[game.Id]);
    }
    
    public async Task AddUser(string userXUId, string gameId)
    {
        queues[gameId]?.Add(userXUId);
        await Clients.All.ChangeQueue(gameId, queues[gameId]);
        var thatGame = ctx.Games.First(k => k.Id == gameId);
        if (queues[gameId]?.Count > thatGame.MinPlayersCount)
        {
            var thisServer = ctx.Servers.First(k => k.GameId == gameId);
            thisServer.IsInUse = true;
            ctx.Servers.Update(thisServer);
        }
    }

    public async Task RemoveUser(string userXUId, string gameId)
    {
        queues[gameId]?.Remove(userXUId);
        await Clients.All.ChangeQueue(gameId, queues[gameId]);
    }

    // public async Task StartCountdown(string gameId, Server server)
    // {
    //     await Clients.All.StartCountdown(gameId, queues[gameId], server);
    // }
    // public async Task StopCountdown(string gameId)
    // {
    //     await Clients.All.StopCountdown(gameId, queues[gameId]);
    // }
}
