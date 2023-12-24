using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiApi.Database;

namespace MultiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class GamesController(AppDbContext ctx) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames()
        => Ok(ctx.Games);
}
