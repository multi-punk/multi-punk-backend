using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using App.Contracts.Statistics;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class StatisticController(IStatisticReader statisticReader, IStatisticWriter statisticWriter) : ControllerBase
{
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetStatisticByGame(string gameId)
        => Ok(await statisticReader.GetStatisticByGameId(gameId));

    [HttpPut]
    public async Task<IActionResult> EditStatistic([FromBody]Statistic statistic)
    {
        await statisticWriter.EditStatistic(statistic);
        return Ok();
    }
}
