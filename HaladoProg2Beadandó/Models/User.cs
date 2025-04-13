using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models
{
    public class User
    {
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public VirtualWallet VirtualWallet { get; set; }

    }
}
