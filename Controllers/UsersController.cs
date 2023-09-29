using Microsoft.AspNetCore.Mvc;
using multi_api.Database;
using multi_api.Database.Tables;
using Microsoft.AspNetCore.Routing;

namespace multi_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private AppDbContext dbContext;

    public UsersController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(dbContext.Users.ToArray());
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        User? user = await dbContext.Users.FindAsync(userId);
        if(user != null)
            return Ok(user);
        else 
            return BadRequest("no such user here");
    }

    [HttpPut]
    public async Task<IActionResult> EditUser([FromBody]User user)
    {
        Party? dbUser= await dbContext.Party.FindAsync(user.Id);
        if(dbUser != null)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else    
            return BadRequest("no such user here to edit");
    }

    [HttpGet("{userId}/party")]
    public async Task<IActionResult> GetUsersParty(string userId)
    {
        User? user = await dbContext.Users.FindAsync(userId);
        if(user != null)
        {
            Party? party = await dbContext.Party.FindAsync(user.PartyId);
            if(party == null)
                BadRequest("user have no party");
            return Ok(party);
        }
        else 
            return BadRequest("no such user here");
    }

    [HttpGet("{userId}/role")]
    public async Task<IActionResult> GetUsersRole(string userId)
    {
        User? user = await dbContext.Users.FindAsync(userId);
        if(user != null)
        {
            Role? role = await dbContext.Roles.FindAsync(user.RoleId);
            if(role == null)
                BadRequest("user have no party");
            return Ok(role);
        }
        else 
            return BadRequest("no such user here");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]User user)
    {
        User? dbUser = await dbContext.Users.FindAsync(user.Id);
        if( dbUser == null)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else 
            return BadRequest("current user already exists");
    }
}
