using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using multi_api.Database;
using multi_api.Database.Tables;

namespace multi_api.Controllers;

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
            return BadRequest("no such party here");
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
