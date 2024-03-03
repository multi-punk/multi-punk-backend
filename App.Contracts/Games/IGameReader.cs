using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Games;

public interface IGameReader
{
    Task<IEnumerable<Game>> GetAllGames();
    Task<Game> GetGameById(string gameId);
}
