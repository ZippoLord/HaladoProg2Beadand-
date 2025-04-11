using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.AspNetCore.Components.Forms;


namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly DataContext _context;
        public CryptoController(DataContext context)
        {
            _context = context;
        }
        //register user
        [HttpPost("register")]
        public async Task<JsonResult> RegisterUser(UserDTO userDTO)
        {
            if (userDTO.UserId == 0)
            {
                var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                var existedName = await _context.Users.FirstOrDefaultAsync(n => n.Name == userDTO.Name);
                if (existedEmail != null)
                    return new JsonResult(BadRequest("Ez az email cím már foglalt"));
                if(existedName!= null)
                    return new JsonResult(BadRequest("Van ilyen nevű felhasználó"));

                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                var wallet = new VirtualWallet
                {
                    Amount = 0,
                    Currency = "Bitcoin",
                    User  = user
                };


                _context.Users.Add(user);
                 _context.VirtualWallets.Add(wallet);
            }
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        [HttpGet("{userId}")]
        public JsonResult GetUsersData(int id)
        {
            var result = _context.Users.Find(id);
            if (result == null)
            return new JsonResult(NotFound());
             return new JsonResult(Ok(result));
        }


        [HttpDelete("{userId}")]
        public JsonResult DeleteSelectedUser(int id)
        {
            var result = _context.Users.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            _context.Users.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok($"Sikeresen törölve lett a {result} id-jű user."));
        }


        [HttpPut("{userId}")]
        public JsonResult EditSelectedUser(int id, [FromBody] User user)
        {
            var result = _context.Users.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            result.Name = user.Name;
            result.Email = user.Email;
            result.Password = user.Password;
            _context.SaveChanges();
            return new JsonResult(Ok($"Sikeresen módosítva lett a {id} id-jű user."));
        }

    }


}
