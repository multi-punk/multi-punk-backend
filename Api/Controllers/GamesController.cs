using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using App.Contracts.Games;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class GamesController(IGameReader gameReader, IGameWriter gameWriter) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames()
        => Ok(await gameReader.GetAllGames());

    [HttpPost]
    public async Task<IActionResult> AddGames([FromBody] Game[] games)
    {
        await gameWriter.AddGames(games);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveGames([FromBody] string[] gamesIds)
    {
        await gameWriter.RemoveGames(gamesIds);
        return Ok();
    }
}
