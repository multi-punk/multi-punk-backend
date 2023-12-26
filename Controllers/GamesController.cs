using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class GamesController(AppDbContext ctx) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames()
        => Ok(ctx.Games);

    [HttpPost]
    public async Task<IActionResult> AddGames([FromBody] Game[] games)
    {
        await ctx.Games.AddRangeAsync(games); 
        await ctx.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveGames([FromBody] string[] gamesIds)
    {
        await ctx.Games.Where(x => gamesIds.Contains(x.Id)).ExecuteDeleteAsync();
        return Ok();
    }
}
