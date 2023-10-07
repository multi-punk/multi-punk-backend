using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PartyController : ControllerBase
{
    private AppDbContext dbContext;

    public PartyController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParty()
    {
        return Ok(dbContext.Party.ToArray());
    }

    [HttpGet("{partyId}/participants")]
    public async Task<IActionResult> GetAllParticipants(string partyId)
    {
        Party? party = await dbContext.Party.FindAsync(partyId);
        if(party != null)
        {
            if(party.Participants == null && party.Participants.Length < 0)
            {
                dbContext.Remove(party);
                return BadRequest("user have no permissions");
            }
            User[] users = dbContext.Users.Where(participant => party.Participants.Contains(participant.Id)).ToArray();
            return Ok(users);
        }
        else 
            return BadRequest("no such party here");
    }

    [HttpPut]
    public async Task<IActionResult> EditParty([FromBody]Party party)
    {
        Party? dbParty = await dbContext.Party.FindAsync(party.Id);
        if(dbParty != null)
        {
            dbContext.Party.Update(party);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else    
            return BadRequest("no such party here to edit");
    }

    [HttpPost]
    public async Task<IActionResult> CreateParty([FromBody]Party party)
    {
        Party? dbParty = await dbContext.Party.FindAsync(party.Id);
        if(dbParty == null)
        {
            await dbContext.Party.AddAsync(party);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else
            return BadRequest("current party already exists");
    }

    [HttpDelete("{partyId}")]
    public async Task<IActionResult> DeleteParty(string partyId)
    {
        Party? party = await dbContext.Party.FindAsync(partyId);
        if(party != null)
        {
            dbContext.Party.Remove(party);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else    
            return BadRequest("no such party here");
    }
}
