using System.Text.Json.Serialization;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class BuyCryptoDTO
    {
        public string Symbol { get; set; }

        public string CryptoCurrencyName { get; set; }
        public double AmountToBuy { get; set; }
    }
}
