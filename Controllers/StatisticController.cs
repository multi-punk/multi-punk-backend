using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class StatisticController(AppDbContext ctx) : ControllerBase
{
    [HttpGet("{game}")]
    public async Task<IActionResult> GetStatisticByGame(string game)
    {
        var statistic = ctx.Statistics.Where(x => x.Game == game);
        return Ok(statistic);
    }
}
