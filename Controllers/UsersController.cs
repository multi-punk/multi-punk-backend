using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext ctx) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(ctx.Users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        User? user = await ctx.Users.FindAsync(userId);
        if(user == null)
            return BadRequest("no such user here");

        return Ok(user);       
    }

    [HttpPut]
    public async Task<IActionResult> EditUser([FromBody]User user)
    {
        if(!ctx.Users.Any(p => p.Id == user.Id))
            return BadRequest("no such user here to edit");

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();

        return Ok();       
    }

    [HttpGet("{userId}/party")]
    public async Task<IActionResult> GetUsersParty(string userId)
    {
        User? user = await ctx.Users.FindAsync(userId);
        if(user == null)
            return BadRequest("no such user here");

        Party? party = await ctx.Party.FindAsync(user.PartyId);
        if(party == null)
            BadRequest("user have no party");

        return Ok(party);   
    }

    [HttpGet("{userId}/role")]
    public async Task<IActionResult> GetUsersRole(string userId)
    {
        User? user = await ctx.Users.FindAsync(userId);
        if(user == null)
            return BadRequest("no such user here");

        Role? role = await ctx.Roles.FindAsync(user.RoleId);
        if(role == null)
            return BadRequest("user have no party");

        return Ok(role);
    }

    [HttpGet("{userId}/permissions")]
    public async Task<IActionResult> GetUsersPermissions(string userId)
    {
        User? user = await ctx.Users.FindAsync(userId);
        if(user == null)
            return BadRequest("no such user here");

        if(user.Permissions == null || user.Permissions.Length < 0)
            return BadRequest("user have no permissions");

        IEnumerable<Permission> permissions = ctx.Permissions.Where(permission => user.Permissions.Contains(permission.Id));

        return Ok(permissions);    
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    {
        if(ctx.Users.Any(p => p.Id == user.Id))
            return BadRequest("current user already exists");
            
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return Ok();   
    }
}
