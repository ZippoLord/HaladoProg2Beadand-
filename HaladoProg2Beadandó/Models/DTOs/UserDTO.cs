namespace HaladoProg2Beadandó.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
