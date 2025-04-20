using System.Text.Json.Serialization;

namespace HaladoProg2Beadandó.Models.DTOs.BuyAndSell
{
    public class BuyCryptoDTO
    {
        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set => _symbol = value?.ToUpper();
        }
        public string CryptoCurrencyName { get; set; }
        public double AmountToBuy { get; set; }
    }
}
