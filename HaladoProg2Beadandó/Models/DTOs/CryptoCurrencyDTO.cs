using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class CryptoCurrencyDTO
    {

        [Required]
        [StringLength(3, ErrorMessage = "A szimbólum legfeljebb 3 karakter lehet.")]
        public string Symbol { get; set; } = null!;

        [Required]
        public string CryptoCurrencyName { get; set; } = null!;


        [Range(100, 1000, ErrorMessage = "Az értéknek 100 és 1000 között kell lennie.")]
        public double Price { get; set; }

        [Range(100, 1000, ErrorMessage = "Az értéknek 100 és 1000 között kell lennie.")]
        public double Amount { get; set; }
    }
}
