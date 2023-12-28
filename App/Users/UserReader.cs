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
        => ctx.Users;

    public async Task<User> GetUser(string userId)
    {
        User? user = await ctx.Users.FindAsync(userId);
        if(user is null)
            throw new ArgumentNullException("no such user here");
        return user;
    }
}
