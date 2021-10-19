using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTReact.Models;

namespace JWTReact.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
    }
}
