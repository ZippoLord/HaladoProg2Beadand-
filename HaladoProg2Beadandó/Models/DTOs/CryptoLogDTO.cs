namespace HaladoProg2Beadandó.Models.DTOs
{
    public class CryptoLogDTO
    {
        public int CryptoCurrencyId { get; set; }
        public string CryptoCurrencyName { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public double Amount { get; set; }

        public DateTime Date { get; set; }

    }
}
