using AutoMapper.Configuration.Annotations;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;

namespace HaladoProg2Beadandó.Services
{
    public class CryptoBGS : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CryptoBGS(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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
                        var priceChange = rand.NextDouble() * 0.1 - 0.05; 
                        var newPrice = crypto.Price * (1 + priceChange / 100);
                        crypto.Price = Math.Round(newPrice, 2);

                        await context.SaveChangesAsync(stoppingToken);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
