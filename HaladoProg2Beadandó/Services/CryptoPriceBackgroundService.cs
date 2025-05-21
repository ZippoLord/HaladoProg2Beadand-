using AutoMapper.Configuration.Annotations;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HaladoProg2Beadandó.Services
{
   
    public class CryptoPriceBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CryptoPriceBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var cryptoPriceService = scope.ServiceProvider.GetRequiredService<ICryptoPriceChange>();

                    await cryptoPriceService.AutoChangePricesAsync(stoppingToken);
                    await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error van a serviceben: {ex.Message}");
            }

        }
    }
}
