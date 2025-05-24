using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs.Transaction;
using Microsoft.EntityFrameworkCore;
using static HaladoProg2Beadandó.Services.TransactionService;



namespace HaladoProg2Beadandó.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionListDTO>> GetTransactionsByUserId(int userId);
        Task<TransactionDTO> GetTransactionsByTransactionId(int transactionId);
    }

    public class TransactionService : ITransactionService

    {

        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionListDTO>> GetTransactionsByUserId(int userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.Date)
                .Select(t => new TransactionListDTO
                {
                    TransactionId = t.TransactionId,
                    Status = t.Status,
                    Date = t.Date
                })
                .ToListAsync();
            return transactions;
        }

        public async Task<TransactionDTO> GetTransactionsByTransactionId(int transactionId)
        {
            var transaction = await _context.Transactions
                .Where(t => t.TransactionId == transactionId)
                .Select(t => new TransactionDTO
                {
                    TransactionId = t.TransactionId,
                    UserId = t.UserId,
                    Symbol = t.Symbol,
                    CryptoCurrencyName = t.CryptoCurrencyName,
                    Amount = t.Amount,
                    Price = t.Price,
                    Date = t.Date,
                    Status = t.Status
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
                throw new InvalidOperationException("Nincs ilyen tranzakció ezzel az ID-vel");
            

            return transaction;
        }
    }
}
