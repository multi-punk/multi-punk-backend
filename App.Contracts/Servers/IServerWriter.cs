using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Contracts.Servers;

public interface IServerWriter
{
    Task ReserveServer(int id);
    Task ExemptServer(int id);
}
