
using HaladoProg2Beadandó.Entities;
using System.Text.Json.Serialization;

namespace HaladoProg2Beadandó.Models
{
    public class CryptoCurrency
    {
        public int CryptoCurrencyId { get; set; }
        
        public string Symbol { get; set; } = null!;
        public string CryptoCurrencyName { get; set; } = null!;
        
        public double Price { get; set; }

        public double Amount { get; set; }
        
        
        [JsonIgnore]
        public List<CryptoPriceHistory> PriceHistory { get; set; } = [];
    }
}
