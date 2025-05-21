namespace HaladoProg2Beadandó.Models.DTOs.CryptoPrice
{
    public class CryptoPriceChangeDTO
    {
        public int CryptoCurrencyId { get; set; }
        public string CryptoCurrencyName { get; set; }
        public string Symbol { get; set; }
        public double NewPrice { get; set; }
        public double ChangePercent { get; set; }

    }
}
