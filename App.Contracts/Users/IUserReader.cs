using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Users;

public interface IUserReader
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUser(string userId);
}
