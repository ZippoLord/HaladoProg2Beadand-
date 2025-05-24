
using HaladoProg2Beadandó.Models;

namespace HaladoProg2Beadandó.Entities
{
    public class CryptoPriceHistory
    {
        public int CryptoPriceHistoryId { get; set; }

        public int CryptoCurrencyId { get; set; }

        public double Price { get; set; }

        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

        public CryptoCurrency CryptoCurrency { get; set; } = null!;
    }
}
