using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Games;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Games;

public class GameReader(AppDbContext ctx) : IGameReader
{
    public async Task<IEnumerable<Game>> GetAllGames()
        => ctx.Games;
}
