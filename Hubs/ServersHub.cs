using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MultiApi.Hubs;

public class ServersHub: Hub
{

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Users");
    }

    public async Task AddUser(string userXUId, string game)
    {
        await Clients.All.SendAsync("Users", game);
    }
    public async Task RemoveUser(string userXUId, string game)
    {
        await Clients.All.SendAsync("Users", game);
    }
}
