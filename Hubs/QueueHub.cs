using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Hubs;

public sealed class QueueHub(AppDbContext ctx, List<User> users): Hub
{
    public async Task AddUser(string userXUId, string game)
    {
        users.Add(new User{
            XUId = userXUId
        });
        await Clients.All.SendAsync("AddUser", users);
    }
    public async Task RemoveUser(string userXUId, string game)
    {
        users.RemoveAll(x => x.XUId == userXUId);
        await Clients.All.SendAsync("RemoveUser", users);
    }
}
