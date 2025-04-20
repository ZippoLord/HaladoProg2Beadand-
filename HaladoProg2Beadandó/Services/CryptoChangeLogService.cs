using HaladoProg2Beadandó.Models.DTOs;
using System.Text.Json;

namespace HaladoProg2Beadandó.Services
{
    public class CryptoChangeLogService
    {
        private string log = "DataLogs/crypto_price_change_log.json";
        public List<CryptoLogDTO> AllLogs()
        {
            if (!File.Exists(log))
                return new List<CryptoLogDTO>();

            var content = File.ReadAllText(log);

            if (string.IsNullOrWhiteSpace(content))
                return new List<CryptoLogDTO>();

            try
            {
                return JsonSerializer.Deserialize<List<CryptoLogDTO>>(content) ?? new List<CryptoLogDTO>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON beolvasási hiba: {ex.Message}");
                return new List<CryptoLogDTO>();
            }
        }


            public void AllLogs(CryptoLogDTO cryptoLog)
        {
            var logs = AllLogs();

            logs.Add(cryptoLog);

            var content = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(log, content);
        }

    }
    }

