using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using HaladoProg2Beadandó.Models.DTOs.CryptoPrice;
using HaladoProg2Beadandó.Entities;
using BCrypt.Net;

namespace HaladoProg2Beadandó.Services
{

    public interface ICryptoPriceChange
    {
        Task AutoChangePricesAsync(CancellationToken stoppingToken);
        Task<CryptoPriceChangeDTO> ManualCryptoPrice(int cryptoId, ModifyCryptoPriceDTO dto);
        Task<List<CryptoPriceChangeDTO>> HistoryCryptoPricesAsync(int cryptoId);
    }
    public class CryptoPriceChangeService : ICryptoPriceChange
    {
        private readonly DataContext context;

        public CryptoPriceChangeService(DataContext context)
        {
            this.context = context;
        }



        public async Task<CryptoPriceChangeDTO> ManualCryptoPrice(int cryptoId, ModifyCryptoPriceDTO dto)
        {
            var crypto = await context.CryptoCurrencies
            .FirstOrDefaultAsync(x => x.CryptoCurrencyId == cryptoId);

            if (crypto == null)
                throw new KeyNotFoundException("Crypto currency not found.");


            context.CryptoPriceHistories.Add(new CryptoPriceHistory
            {
                CryptoCurrencyId = cryptoId,
                Price = dto.newPrice,
                LoggedAt = DateTime.UtcNow
            });

            // Friss ár mentése (ha van ilyen meződ)
            crypto.Price = dto.newPrice;

            await context.SaveChangesAsync();

            return new CryptoPriceChangeDTO
            {
                CryptoCurrencyId = crypto.CryptoCurrencyId,
                CryptoCurrencyName = crypto.CryptoCurrencyName,
                Symbol = crypto.Symbol,
                NewPrice = dto.newPrice,
                Date = DateTime.UtcNow
            };
        }

        public async Task AutoChangePricesAsync(CancellationToken stoppingToken)
        {

            var cryptos = await context.CryptoCurrencies.ToListAsync(stoppingToken);
            var rand = new Random();
            foreach (var crypto in cryptos)
            {
                var priceChange = rand.NextDouble() * 0.3 - 0.10;
                var newPrice = crypto.Price * (1 + priceChange / 100);
                crypto.Price = Math.Round(newPrice, 2);
                context.CryptoCurrencies.Update(crypto);
                await context.CryptoPriceHistories.AddAsync(new CryptoPriceHistory
                {
                    CryptoCurrencyId = crypto.CryptoCurrencyId,
                    Price = crypto.Price
                }, stoppingToken);

            }
            await context.SaveChangesAsync(stoppingToken);
        }

        public async Task<List<CryptoPriceChangeDTO>> HistoryCryptoPricesAsync(int cryptoId)
        {
            var list = await context.CryptoPriceHistories.Where(x => x.CryptoCurrencyId == cryptoId)
                .OrderByDescending(x => x.LoggedAt)
                .Select(x => new CryptoPriceChangeDTO
                {
                    CryptoCurrencyId = x.CryptoCurrencyId,
                    CryptoCurrencyName = x.CryptoCurrency.CryptoCurrencyName,
                    Symbol = x.CryptoCurrency.Symbol,
                    NewPrice = x.Price,
                    Date = x.LoggedAt.ToLocalTime()

                })
                .ToListAsync();
            return list;
        }
    }
}
