using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Hubs;

[AllowAnonymous]
public sealed class QueueHub: Hub
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
            await Clients.Caller.SendAsync("UsersInQueue", game.Id, queues[game.Id]);
    }
    public async Task AddUser(string userXUId, string game)
    {
        queues[game]?.Add(userXUId);
        await Clients.All.SendAsync("UsersInQueue", game, queues[game]);
    }
    public async Task RemoveUser(string userXUId, string game)
    {
        queues[game]?.Remove(userXUId);
        await Clients.All.SendAsync("UsersInQueue", game, queues[game]);
    }
}
