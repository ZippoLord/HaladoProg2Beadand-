namespace HaladoProg2Beadandó.Models.DTOs.Transaction
{
    public class TransactionDetailsDTO
    {
        public double Price { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public string Symbol { get; set; } = null!;

        public string CryptoCurrencyName { get; set; } = null!;

        public string Status { get; set; }
    }
}
