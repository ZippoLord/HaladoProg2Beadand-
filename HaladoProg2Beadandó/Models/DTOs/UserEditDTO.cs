using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class UserEditDTO
    {

        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
