using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.User;
using HaladoProg2Beadandó.Services;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //register user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDTO userDTO)
        {
            try
            { 
                await _userService.CreateUserAsync(userDTO);
                return Ok("Sikeres regisztráció");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUser(int userId)
        {

            try {
                var result = await _userService.GetUserByAsync(userId);
                return Ok(result);                
            }
            catch(Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> EditUser(int userId, EditUserDTO userDTO)
        {

            try
            {
                var result = await _userService.EditUserByAsync(userId, userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteSelectedUser(int userId)
        {
            try
            {
                await _userService.DeleteUserByAsync(userId);
                return Ok("Sikeresen törölted a felhasználót"); 
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt: {ex.Message}");
            }
        }

    }
}
