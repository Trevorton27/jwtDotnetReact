using JWTReact.Data;
using JWTReact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTReact.Services
{
    public class UserService : IUserService
    {

        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();

            return user;
        }

        public User GetAllUsers(User users)
        {
            throw new NotImplementedException();
        }

        // public class GetAllUsers(User user)
        // {
        //     return _context.Users;
        // }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
