namespace HaladoProg2Beadandó.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; }

        public string CryptoCurrencyName { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
