using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Games;

public interface IGameWriter
{
    Task AddGames(params Game[] games);
    Task RemoveGames(params string[] gamesIds);
}
