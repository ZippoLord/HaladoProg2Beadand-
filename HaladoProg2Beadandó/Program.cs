
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.MapperConfigs;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Service;
using HaladoProg2Beadandó.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;

namespace HaladoProg2Beadandó
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=localhost;Database=CryptoDb_ARZ5PC;Trusted_Connection=True;TrustServerCertificate=True"));


            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);


            builder.Services.AddHostedService<CryptoPriceBackgroundService>();
            builder.Services.AddSingleton<TransactionLogService>();
            builder.Services.AddSingleton<CryptoChangeLogService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<ISellBuyCryptoService, SellBuyCryptoService>();

            builder.Services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();

            builder.Services.AddScoped<ICryptoPriceChange, CryptoPriceChangeService>();
            builder.Logging.AddConsole();
            var app = builder.Build();



            //példaadatok
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                DummyDatas.SeedDatabase(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
