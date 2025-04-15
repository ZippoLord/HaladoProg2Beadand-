
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.MapperConfigs;
using HaladoProg2Beadandó.Services;
using Microsoft.EntityFrameworkCore;

namespace HaladoProg2Beadandó
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=(local);Database=CryptoDb_ARZ5PC;Trusted_Connection=True;TrustServerCertificate=True"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddHostedService<CryptoBGS>();
            var app = builder.Build();

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
