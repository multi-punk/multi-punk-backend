using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Servers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class ServersController(ServerWriter serverWriter) : ControllerBase
{
    [HttpGet("{serverId}/reserve")]
    public async Task<IActionResult> ReserveServer(int serverId)
    {
        await serverWriter.ReserveServer(serverId);
        return Ok();
    }

    [HttpGet("{serverId}/exempt")]
    public async Task<IActionResult> ExemptServer(int serverId)
    {
        await serverWriter.ExemptServer(serverId);
        return Ok();
    }
}
