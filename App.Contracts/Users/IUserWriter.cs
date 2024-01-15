using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database.Tables;

namespace App.Contracts.Users;

public interface IUserWriter
{
    Task CreateUser(User user);
    Task EditUser(User user);
}
