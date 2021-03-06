using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTReact.Auth;
using JWTReact.Data;
using JWTReact.Dtos;
using JWTReact.Models;
using JWTReact.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTReact.Controllers
{

    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
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


            return Created(uri: "Success bananas!", value: _userService.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _userService.GetByEmail(dto.Email);
            if (user == null) return BadRequest(error: new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(text: dto.Password, hash: user.Password))
            {
                return BadRequest(error: new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            return Ok(jwt);
        }

        [HttpGet("user")]
        public new IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);
                var user = _userService.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        // [HttpGet("allusers")]
        // public IActionResult GetAllUsers()
        // {

        // }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Successfully logged out."
            });
        }

    }
}
