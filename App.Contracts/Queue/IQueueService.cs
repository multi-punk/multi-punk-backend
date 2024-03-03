using App.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Contracts;

public interface IQueueService
{
    Task AddUser(string gameId, params string[] userXUId);
    Task RemoveUser(string gameId, params string[] userXUid);
    Task RemoveUserFromAllGames(string userXUId);
}
