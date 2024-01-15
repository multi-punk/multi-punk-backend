using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using App.Contracts.Hubs;
using Infrastructure.Database;
using App.Contracts;

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
        queues = dataProvider.Queues;
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

    // public async Task StartCountdown(string gameId, Server server)
    // {
    //     await Clients.All.StartCountdown(gameId, queues[gameId], server);
    // }
    // public async Task StopCountdown(string gameId)
    // {
    //     await Clients.All.StopCountdown(gameId, queues[gameId]);
    // }
}
