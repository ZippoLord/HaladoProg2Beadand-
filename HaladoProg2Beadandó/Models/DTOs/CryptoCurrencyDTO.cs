namespace HaladoProg2Beadandó.Models.DTOs
{
    public class CryptoCurrencyDTO
    {
        public string Symbol { get; set; } = null!;
        public string CryptoCurrencyName { get; set; } = null!;

        public double Price { get; set; }

        public double Amount { get; set; }
    }
}
