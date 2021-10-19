using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTReact.Data;
using JWTReact.Dtos;
using JWTReact.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTReact.Controllers
{

    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)

            };


            return Created(uri: "Success bananas!", value: _repository.Create(user));
        }
    }
}
