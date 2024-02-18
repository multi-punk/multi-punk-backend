using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using App.Contracts.Hubs;
using Infrastructure.Database;
using App.Contracts;
using Domain;

namespace Api.Hubs;

[AllowAnonymous]
public sealed class QueueHub: Hub<IQueueHub>
{
    private Dictionary<string, List<string>> queues;
    private AppDbContext ctx;
    private IQueueService service;

    public QueueHub(TempDataProvider dataProvider, AppDbContext ctx, IQueueService service)
    {
        this.service = service;
        queues = dataProvider.UsersInGames;
        this.ctx = ctx;
    }

    public override async Task OnConnectedAsync()
    {
        foreach(var game in ctx.Games)
            await Clients.Caller.ChangeQueue(game.Id, queues[game.Id]);
    }
    
    public async Task AddUser(string userXUId, string gameId)
        => await service.AddUser(userXUId, gameId);

    public async Task RemoveUser(string userXUId, string gameId)
        => await service.RemoveUser(userXUId, gameId);
}
