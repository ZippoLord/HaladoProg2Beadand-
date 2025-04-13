using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : DataContextController
    {
        public UserController(DataContext context) : base(context) { }
        //register user
        [HttpPost("register")]
        public async Task<JsonResult> RegisterUser(UserDTO userDTO)
        {
           
                var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if (existedEmail != null)
                    return new JsonResult(BadRequest("Ez az email cím már foglalt"));

                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                _context.Users.Add(user);            
            _context.SaveChanges();
            return new JsonResult(Ok("Sikeresen hozzáadva a felhasználó"));
        }

        [HttpGet("{userId}")]
        public JsonResult GetUsersData(int userId)
        {
            var result = _context.Users.Find(userId);
            if (result == null)
                return new JsonResult(NotFound());
            return new JsonResult(Ok(result));
        }


        [HttpDelete("{userId}")]
        public JsonResult DeleteSelectedUser(int userId)
        {
            var result = _context.Users.Find(userId);
            if (result == null)
                return new JsonResult(NotFound());
            _context.Users.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok($"Sikeresen törölve lett a {userId} id-jű user."));
        }


        [HttpPut("{userId}")]
        public async Task<JsonResult> EditSelectedUser(int userId, [FromBody] UserDTO userDTO)
        {
            var result = await _context.Users.FindAsync(userId);
            if (result == null)
                return new JsonResult(NotFound());

            var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if(existedEmail != null) 
                return new JsonResult(BadRequest("Ez az email cím már foglalt"));

            result.Name = userDTO.Name;
            result.Email = userDTO.Email;
            result.Password = userDTO.Password;
            await _context.SaveChangesAsync();
            return new JsonResult(Ok($"Sikeresen módosítva lett a {userId} id-jű user."));
        }
    }
}
