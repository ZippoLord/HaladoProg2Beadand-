using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models.DTOs.User
{
    public class EditUserDTO
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }   
    }
}
