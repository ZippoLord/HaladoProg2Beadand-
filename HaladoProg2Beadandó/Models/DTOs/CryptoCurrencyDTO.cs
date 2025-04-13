using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class CryptoCurrencyDTO
    {

        [Required]
        [StringLength(7, ErrorMessage = "The password must be 7 characters long")]
        public string Symbol { get; set; } = null!;


        [Required]
        public string CryptoCurrencyName { get; set; } = null!;

        [Required] 
        [Range(50, 1000, ErrorMessage = "Az értéknek 50 és 1000 között kell lennie.")]
        public double Price { get; set; }

        [Required]
        [Range(50, 1000,  ErrorMessage = "Az értéknek 50 és 1000 között kell lennie.")]
        public double Amount { get; set; }
    }
}
