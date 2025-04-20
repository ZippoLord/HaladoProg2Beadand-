using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models.DTOs.User
{
    public class UserRegisterDTO
    {

        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "The password must be 7 characters long")]
        public string Password { get; set; } = null!;
    }
}
