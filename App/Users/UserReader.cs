using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Users;
using Infrastructure.Database;
using Infrastructure.Database.Tables;

namespace App.Users;

public class UserReader(AppDbContext ctx) : IUserReader
{
    public async Task<IEnumerable<User>> GetAllUsers()
        => ctx.Users.ToList();

    public async Task<User> GetUser(string userId)
    {
        User user = await ctx.Users.FindAsync(userId);
        return user;
    }
}
