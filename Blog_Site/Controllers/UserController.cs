using Blog_Site.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> UserRegister(User user)
        {
            user = await _userRepository.RegisterAsync(user);
            if(user == null)
            {
                return BadRequest("User Exists");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn(string userName, string password)
        {
            string token = await _userRepository.LoginAsync(userName, password);
            if(token == null)
            {
                return BadRequest("No user exists");
            }
            return Ok(token);
        }

        [HttpPut("edit_name")]
        public async Task<ActionResult<User>> EditName(User user)
        {
            user = await _userRepository.EditNameAsync(user);
            if (user == null)
            {
                return BadRequest("Wrong User");
            }

            return Ok(user);
        }

        [HttpPut("edit_password")]
        public async Task<ActionResult<User>> EditPassword(User user)
        {
            user = await _userRepository.EditPasswordAsync(user);
            if (user == null)
            {
                return BadRequest("Wrong User");
            }

            return Ok(user);
        }
    }
}
