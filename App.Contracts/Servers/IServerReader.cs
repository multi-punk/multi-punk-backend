using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Contracts.Servers;

public interface IServerReader
{
    Task GetFreeServer(string gameId);
}
