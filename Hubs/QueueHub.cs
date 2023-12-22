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
    private Dictionary<string, List<string>> PlayersInQueue;

    public QueueHub(TempDataProvider dataProvider)
    {
        PlayersInQueue = dataProvider.PlayersInQueue;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Users", PlayersInQueue);
    }
    public async Task AddUser(string userXUId, string game)
    {
        PlayersInQueue[game]?.Add(userXUId);
        await Clients.All.SendAsync("Users", PlayersInQueue);
    }
    public async Task RemoveUser(string userXUId, string game)
    {
        PlayersInQueue[game]?.Remove(userXUId);
        await Clients.All.SendAsync("Users", PlayersInQueue);
    }
}
