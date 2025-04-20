using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HaladoProg2Beadandó.Models.DTOs.BuyAndSell
{
    public class SellCryptoDTO
    {
        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set => _symbol = value?.ToUpper();
        }
        public string CryptoCurrencyName { get; set; }
        public double AmountToSell { get; set; }
    }

}

