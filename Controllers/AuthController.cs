using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTReact.Auth;
using JWTReact.Data;
using JWTReact.Dtos;
using JWTReact.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTReact.Controllers
{

    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
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

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.GetByEmail(dto.Email);
            if (user == null) return BadRequest(error: new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(text: dto.Password, hash: user.Password))
            {
                return BadRequest(error: new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                jwt
            });
        }
    }
}
