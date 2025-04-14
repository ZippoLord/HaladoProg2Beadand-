using System.Text.Json.Serialization;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class BuyCryptoDTO
    {
        public int UserId { get; set; }
        public string Symbol { get; set; }

        public string CryptoCurrencyName { get; set; }
        public double AmountToBuy { get; set; }
    }
}
