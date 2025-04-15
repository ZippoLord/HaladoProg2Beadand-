using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;
using AutoMapper;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : DataContextController
    {
        private readonly IMapper mapper;

        public UserController(DataContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }
        //register user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDTO userDTO)
        {
           
                var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if (existedEmail != null)
                    return (BadRequest("Ez az email cím már foglalt"));

                var user = mapper.Map<User>(userDTO);
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            _context.Users.Add(user);            
            _context.SaveChanges();
            return (Ok("Sikeresen hozzáadva a felhasználó"));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserReadDTO>> GetUsersData(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();
            var result = mapper.Map<UserReadDTO>(user);
            return result;
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteSelectedUser(int userId)
        {
            var result = await _context.Users.FindAsync(userId);
            if (result == null)
                return NotFound();
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Sikeresen törölve lett a {userId} id-jű user.");
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> EditSelectedUser(int userId, [FromBody] UserEditDTO userDTO)
        {
            var result = await _context.Users.FindAsync(userId);
            if (result == null)
                return NotFound();

            var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if(existedEmail != null) 
                return BadRequest("Ez az email cím már foglalt");


            result.Name = userDTO.Name;
            result.Email = userDTO.Email;
            result.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            await _context.SaveChangesAsync();
            return Ok($"Sikeresen módosítva lett a {userId} id-jű user.");
        }
    }
}
