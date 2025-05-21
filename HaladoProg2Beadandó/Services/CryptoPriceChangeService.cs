using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using HaladoProg2Beadandó.Models.DTOs.CryptoPrice;

namespace HaladoProg2Beadandó.Services
{

    public interface ICryptoPriceChange
    {
        Task AutoChangePricesAsync(CancellationToken stoppingToken);
        Task<CryptoCurrency> ManualCryptoPrice(int cryptoId, ModifyCryptoPriceDTO dto);
        Task<List<CryptoPriceChangeDTO>> HistoryCryptoPricesAsync(int cryptoId);
    }
    public class CryptoPriceChangeService : ICryptoPriceChange
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CryptoPriceChangeService> _logger;

        public CryptoPriceChangeService(IServiceScopeFactory scopeFactory, ILogger<CryptoPriceChangeService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task<CryptoCurrency> ManualCryptoPrice(int cryptoId, ModifyCryptoPriceDTO dto)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var crypto = await context.CryptoCurrencies.FindAsync(cryptoId);
            if (crypto == null)
                throw new InvalidOperationException("Nincs ilyen id-jű kriptovaluta");

            crypto.Price = dto.newPrice;
            await context.SaveChangesAsync();
            return crypto;
        }

        public async Task AutoChangePricesAsync(CancellationToken stoppingToken)
        {

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var cryptos = await context.CryptoCurrencies.ToListAsync(stoppingToken);
            var rand = new Random();
            foreach (var crypto in cryptos)
            {
                var priceChange = rand.NextDouble() * 0.3 - 0.10;
                var newPrice = crypto.Price * (1 + priceChange / 100);
                crypto.Price = Math.Round(newPrice, 2);
                context.CryptoCurrencies.Update(crypto);
                var logObject = new
                {
                    CryptoCurrencyId = crypto.CryptoCurrencyId,
                    Symbol = crypto.Symbol,
                    CryptoCurrencyName = crypto.CryptoCurrencyName,
                    NewPrice = crypto.Price,
                    ChangePercent = priceChange
                    //TODO MAYBE DATE
                };

                var json = JsonSerializer.Serialize(logObject);
                await File.AppendAllTextAsync("DataLogs/crypto_price_change_log.jsonl", json + Environment.NewLine);
            }
            await context.SaveChangesAsync(stoppingToken);
        }


                public async Task<List<CryptoPriceChangeDTO>> HistoryCryptoPricesAsync(int cryptoId)
                {
                    using var stream = File.OpenRead("DataLogs/crypto_price_change_log.jsonl");
                    using var reader = new StreamReader(stream);
                    List<CryptoPriceChangeDTO> cryptoPriceChanges = new List<CryptoPriceChangeDTO>();

                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var selectedCrypto = JsonSerializer.Deserialize<CryptoPriceChangeDTO>(line);
                        if (selectedCrypto != null && selectedCrypto.CryptoCurrencyId == cryptoId)
                        {
                            cryptoPriceChanges.Add(selectedCrypto);
                        }
                    }

                    if (cryptoPriceChanges.Count == 0)
                        throw new InvalidOperationException("Nincs ilyen id-jű kriptovaluta");

                    return cryptoPriceChanges;
                }
    }
}
