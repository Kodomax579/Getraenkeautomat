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
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private Service _userService;
        public UserController(ILogger<UserController> logger, Service userService)
        {
            this._logger = logger;
            this._userService = userService;
        }

        [HttpGet("/Login")]
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
                return BadRequest("User not found");
            }

            return Ok(user);
        }

        [HttpPost("/CreateUser")]
        public async Task<ActionResult<UserDTO>> Createuser(CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }
            int response = await _userService.CreateUser(userDto);

            switch (response)
            {
                case -1:
                    _logger.LogError("User already exist");
                    return BadRequest("User already exist");
                case -2:
                    _logger.LogError("Body has no Name");
                    return BadRequest("Body has no Name");
                case -3:
                    _logger.LogError("Create bankaccount error");
                    return BadRequest("Create bankaccount error");
                default:
                    var user = this._userService.GetUser(userDto.name, userDto.password);
                    return Ok(user);
            }
        }



        [HttpPut("/UpdateUser")]
        public ActionResult<UserDTO> UpdateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }

            var updatedUser = this._userService.UpdateUser(userDto);
            if (updatedUser == null)
            {
                return BadRequest("User not found");
            }

            return Ok(updatedUser);
        }


    }
}
