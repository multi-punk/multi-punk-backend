using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class StatisticController(AppDbContext ctx) : ControllerBase
{
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetStatisticByGame(string gameId)
    {
        var statistic = ctx.Statistics.Where(x => x.GameId == gameId);
        
        if(statistic.Any())
            return Ok(statistic);
        else
            return BadRequest("no such game here");
    }

    [HttpPut]
    public async Task<IActionResult> EditStatistic([FromBody]Statistic statistic)
    {
        var userStatistic = ctx.Statistics
            .Where(x => x.UserId == statistic.UserId && x.GameId == statistic.GameId)
            .FirstOrDefault();

        if(userStatistic is null)
            return BadRequest("no such user or game");

        await ctx.Statistics.Where(x => x.UserId == statistic.UserId && x.GameId == statistic.GameId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Score, p => p.Score + statistic.Score));

        return Ok();
    }
}
