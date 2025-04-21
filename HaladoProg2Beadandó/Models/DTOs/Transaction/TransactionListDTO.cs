namespace HaladoProg2Beadandó.Models.DTOs.Transaction
{
    public class TransactionListDTO
    {
        public int TransactionId { get; set; }

        public double Price { get; set; }
        public double Amount { get; set; }
    
        public DateTime Date { get; set; }
    }
}
