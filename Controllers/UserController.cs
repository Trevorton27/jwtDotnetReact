using Microsoft.AspNetCore.Mvc;

namespace JWTReact.Controllers
{
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {

        }


    }
}