using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
        private Service _userService;
        public UserController(Service userService)
        {
            this._userService = userService;
        }

        [HttpGet("Login")]
        public ActionResult<UserDTO> GetUserById(string uName, string uPassword)
        {
            if (string.IsNullOrEmpty(uName))
            {
                Log.Error("Username is required");
                return BadRequest("Username is required");
            }
            if (string.IsNullOrEmpty(uPassword))
            {
                Log.Error("Password is required");
                return BadRequest("Password is required");
            }

            var user = _userService.GetUser(uName, uPassword);

            if (user == null)
            {
                Log.Error("username or password wrong");
                return Unauthorized("Benutzername oder Passwort ist falsch.");
            }
            Log.Information("Response succesfull: user:{@user}", user);
            return Ok(user);

        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                Log.Error("Request without body");
                return BadRequest("Request without body");
            }
            int response = await _userService.CreateUser(userDto);

            switch (response)
            {
                case -1:
                    Log.Error("User already exist");
                    return BadRequest("User already exist");
                case -2:
                    Log.Error("Body without name");
                    return BadRequest("Body without name");
                case -3:
                    Log.Error("Create bankaccount error");
                    return BadRequest("Create bankaccount error");
                default:
                    if (string.IsNullOrEmpty(userDto.name))
                    {
                        Log.Error("Request without username");
                        return BadRequest("Request without username");
                    }
                    if (string.IsNullOrEmpty(userDto.password))
                    {
                        Log.Error("Request without password");
                        return BadRequest("Request without password");
                    }
                    var user = this._userService.GetUser(userDto.name, userDto.password);

                    Log.Information("Response user:{@user}", user);

                    return Ok(user);
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult<UserDTO> UpdateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                Log.Error("Request without body");
                return BadRequest("Request without body");
            }

            var updatedUser = this._userService.UpdateUser(userDto);
            if (updatedUser == null)
            {
                Log.Error("User not found");
                return BadRequest("User not found");
            }

            Log.Information("Response updatedUser:{@updatedUser}", updatedUser);
            return Ok(updatedUser);
        }
    }
}
