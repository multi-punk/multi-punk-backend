using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Hubs;
using App.Contracts;
using Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;

namespace App.Models;

public class QueueService(AppDbContext ctx, IHubContext<Hub<IQueueHub>> hub): IQueueService
{
    public async Task ChangeQueue(string userXUId, string gameId)
    {
        
    }
}
