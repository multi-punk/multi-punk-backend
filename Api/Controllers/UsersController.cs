using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using App.Contracts.Users;
using App.Contracts.Statistics;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class UsersController(IUserReader userReader, IUserWriter userWriter, IStatisticReader statisticReader, AppDbContext ctx) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
        => Ok(await userReader.GetAllUsers());

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
        => Ok(await userReader.GetUser(userId));

    [HttpPut]
    public async Task<IActionResult> EditUser([FromBody]User user)
    {
        await userWriter.EditUser(user);
        return Ok();       
    }

    [HttpGet("{userId}/role")]
    public async Task<IActionResult> GetUsersRole(string userId)
    {
        User user = await userReader.GetUser(userId);

        Role? role = await ctx.Roles.FindAsync(user.RoleId);
        if(role == null)
            return BadRequest("user have no party");

        return Ok(role);
    }

    [HttpGet("{userId}/permissions")]
    public async Task<IActionResult> GetUsersPermissions(string userId)
    {
        User user = await userReader.GetUser(userId);

        if(user.Permissions == null || user.Permissions.Length < 0)
            return BadRequest("user have no permissions");

        IEnumerable<Permission> permissions = ctx.Permissions.Where(permission => user.Permissions.Contains(permission.Id));

        return Ok(permissions);    
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    {
        await userWriter.CreateUser(user);
        return Ok();   
    }

    [HttpGet("{userId}/statistic")]
    public async Task<IActionResult> GetUsersStatistic(string userId)
        => Ok(await statisticReader.GetStatisticByUserId(userId));
}
