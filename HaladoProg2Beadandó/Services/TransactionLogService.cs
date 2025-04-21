using HaladoProg2Beadandó.Models.DTOs.Transaction;
using System.Text.Json;

namespace HaladoProg2Beadandó.Services
{
    public class TransactionLogService
    {
        private string log = "DataLogs/transaction_log.json";
        public List<TransactionDTO> AllLogs()
        {
            if (!File.Exists(log))
                return new List<TransactionDTO>();

            var content = File.ReadAllText(log);

            if (string.IsNullOrWhiteSpace(content))
                return new List<TransactionDTO>();

            try
            {
                return JsonSerializer.Deserialize<List<TransactionDTO>>(content) ?? new List<TransactionDTO>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON beolvasási hiba: {ex.Message}");
                return new List<TransactionDTO>();
            }


        }

        public int GenerateTransactionId()
        {
            var transactions = AllLogs();
            if (transactions.Count == 0)
                return 1;
            return transactions.Max(x => x.TransactionId) + 1;
        }

        public void AddLog(TransactionDTO transaction)
        {
            var transactions = AllLogs();

            transaction.TransactionId = GenerateTransactionId();

            transactions.Add(transaction);

            var content = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(log, content);
        }

    }
}

