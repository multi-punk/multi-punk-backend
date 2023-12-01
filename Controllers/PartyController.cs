using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiApi.Database;
using MultiApi.Database.Tables;

namespace MultiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "MULTI-API-KEY-PRIVATE")]
public class PartyController(AppDbContext ctx) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllParty()
    {
        return Ok(ctx.Party);
    }

    [HttpGet("{partyId}")]
    public async Task<IActionResult> GetUser(string partyId)
    {
        Party? party = await ctx.Party.FindAsync(partyId);
        if(party == null)
            return BadRequest("no such party here");

        return Ok(party);       
    }

    [HttpGet("{partyId}/participants")]
    public async Task<IActionResult> GetAllParticipants(string partyId)
    {

        Party? party = await ctx.Party.FindAsync(partyId);
        if(party == null)
            return BadRequest("no such party here");

        if(party.Participants == null || party.Participants.Count < 0)
        {
            ctx.Remove(party);
            return BadRequest("user have no permissions");
        }
        User[] users = ctx.Users.Where(user => party.Participants.Contains(user.Id)).ToArray();
        
        return Ok(users);            
    }

    [HttpPut]
    public async Task<IActionResult> EditParty([FromBody]Party party)
    {
        if(!ctx.Party.Any(p => p.Id == party.Id))
            return BadRequest("no such party here to edit");

        ctx.Party.Update(party);
        await ctx.SaveChangesAsync();
        return Ok();  
    }

    [HttpPost]
    public async Task<IActionResult> CreateParty([FromBody]Party party)
    {
        if(ctx.Party.Any(p => p.Id == party.Id))
            return BadRequest("current party already exists");

        await ctx.Party.AddAsync(party);
        await ctx.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{partyId}")]
    public async Task<IActionResult> DeleteParty(string partyId)
    {
        Party? party = await ctx.Party.FindAsync(partyId);
        if(party == null)
            return BadRequest("no such party here");

        ctx.Party.Remove(party);
        await ctx.SaveChangesAsync();
        return Ok();        
    }
}
