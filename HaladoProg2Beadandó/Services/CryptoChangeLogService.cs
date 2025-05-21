using HaladoProg2Beadandó.Models.DTOs.CryptoPrice;
using System.Text.Json;

namespace HaladoProg2Beadandó.Services
{
    public class CryptoChangeLogService
    {
        private string log = "DataLogs/crypto_price_change_log.json";
        public List<CryptoPriceChangeDTO> AllLogs()
        {
            if (!File.Exists(log))
                return new List<CryptoPriceChangeDTO>();

            var content = File.ReadAllText(log);

            if (string.IsNullOrWhiteSpace(content))
                return new List<CryptoPriceChangeDTO>();

            try
            {
                return JsonSerializer.Deserialize<List<CryptoPriceChangeDTO>>(content) ?? new List<CryptoPriceChangeDTO>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON beolvasási hiba: {ex.Message}");
                return new List<CryptoPriceChangeDTO>();
            }
        }


            public void AllLogs(CryptoPriceChangeDTO cryptoLog)
        {
            var logs = AllLogs();

            logs.Add(cryptoLog);

            var content = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(log, content);
        }

    }
    }

