using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.DTO;
using User.Models;
using User.Services;
using UserService.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private Service _userService;
        public UserController(ILogger<UserController> logger, Service userService)
        {
            this._logger = logger;
            this._userService = userService;
        }

        [HttpGet("Login")]
        public ActionResult<UserDTO> GetUserById(string uName, string uPassword)
        {
            if (string.IsNullOrEmpty(uName))
            {
                _logger.LogError("Username is required");
                return BadRequest("Username is required");
            }
            if (string.IsNullOrEmpty(uPassword))
            {
                _logger.LogError("Password is required");
                return BadRequest("Password is required");
            }

            var user = _userService.GetUser(uName, uPassword);

            if (user == null)
            {
                return Unauthorized("Benutzername oder Passwort ist falsch.");
            }

            return Ok(user);

        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                _logger.LogError("Request without body");
                return BadRequest("Request without body");
            }
            int response = await _userService.CreateUser(userDto);

            switch (response)
            {
                case -1:
                    _logger.LogError("User already exist");
                    return BadRequest("User already exist");
                case -2:
                    _logger.LogError("Body without name");
                    return BadRequest("Body without name");
                case -3:
                    _logger.LogError("Create bankaccount error");
                    return BadRequest("Create bankaccount error");
                default:
                    if (string.IsNullOrEmpty(userDto.name))
                    {
                        _logger.LogError("Request without username");
                        return BadRequest("Request without username");
                    }
                    if (string.IsNullOrEmpty(userDto.password))
                    {
                        _logger.LogError("Request without password");
                        return BadRequest("Request without password");
                    }
                    var user = this._userService.GetUser(userDto.name, userDto.password);

                    _logger.LogInformation("Response user:{user}", user);

                    return Ok(user);
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult<UserDTO> UpdateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                _logger.LogError("Request without body");
                return BadRequest("Request without body");
            }

            var updatedUser = this._userService.UpdateUser(userDto);
            if (updatedUser == null)
            {
                _logger.LogError("User not found");
                return BadRequest("User not found");
            }

            _logger.LogInformation("Response updatedUser:{updatedUser}", updatedUser);
            return Ok(updatedUser);
        }
    }
}
