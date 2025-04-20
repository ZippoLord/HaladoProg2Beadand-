using AutoMapper.Configuration.Annotations;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HaladoProg2Beadandó.Services
{
    public class CryptoBGS : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        
        private readonly ILogger<CryptoBGS> _logger;
        private readonly CryptoChangeLogService _logCrypto;
        public CryptoBGS(IServiceScopeFactory scopeFactory, ILogger<CryptoBGS> logger, CryptoChangeLogService logService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _logCrypto = logService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                { 

                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var cryptos = await context.CryptoCurrencies.ToListAsync(stoppingToken);
                    var rand = new Random();
                        foreach (var crypto in cryptos)
                    {
                        var priceChange = rand.NextDouble() * 0.2 - 0.05;
                        var newPrice = crypto.Price * (1 + priceChange / 100);
                        var oldPrice = crypto.Price;
                        crypto.Price = Math.Round(newPrice, 2);


                        var logEntry = new CryptoLogDTO
                        {
                            CryptoCurrencyId = crypto.CryptoCurrencyId,
                            Symbol = crypto.Symbol,
                            CryptoCurrencyName = crypto.CryptoCurrencyName,
                            Price = crypto.Price,
                            OldPrice = oldPrice,
                            Amount = crypto.Amount,
                            Date = DateTime.Now,
                        };

                        _logCrypto.AllLogs(logEntry);
                        _logger.LogInformation($"[LOG] {crypto.Symbol} ({crypto.CryptoCurrencyName}) ára változott: {oldPrice} → {crypto.Price}");
                        context.CryptoCurrencies.Update(crypto);
                        await context.SaveChangesAsync(stoppingToken);
                    }

                }
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
