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
        public string Password { get; set; } = null!;
    }
}
