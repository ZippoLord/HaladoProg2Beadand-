using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HaladoProg2Beadandó.Models.DTOs
{
    public class SellCryptoDTO
    {
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public string CryptoCurrencyName { get; set; }
        public double AmountToSell { get; set; }
    }

}

