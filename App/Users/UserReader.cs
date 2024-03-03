using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Users;
using Infrastructure.Database;
using Infrastructure.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Users;

public class UserReader(AppDbContext ctx) : IUserReader
{
    public async Task<IEnumerable<User>> GetAllUsers()
        => await ctx.Users.ToListAsync();

    public async Task<User> GetUser(string userId)
        => await ctx.Users.FirstOrDefaultAsync(x => x.Id == userId);
}
