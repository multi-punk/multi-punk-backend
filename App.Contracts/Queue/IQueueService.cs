using App.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Contracts;

public interface IQueueService
{
    Task ChangeQueue(string userXUId, string gameId, ChangeQueueType type);
}
