using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Servers;

public interface IServerReader
{
    Task<Server> GetFreeServer(string gameId);
}
