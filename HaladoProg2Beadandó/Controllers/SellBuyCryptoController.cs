using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs;
using HaladoProg2Beadandó.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json;
using HaladoProg2Beadandó.Services;
using HaladoProg2Beadandó.Models.DTOs.BuyAndSell;
using HaladoProg2Beadandó.Models.DTOs.Wallet;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/trade")]
    [ApiController]
    public class SellBuyCryptoController : DataContextController
    {
        private readonly IMapper mapper;
        private readonly TransactionLogService _logService;
        public SellBuyCryptoController(DataContext context, IMapper mapper, TransactionLogService logService) : base(context) {
            _logService = logService;
            this.mapper = mapper;
        }


        [HttpPost("buy")]
        public async Task<IActionResult> BuyCrypto(int userId, [FromBody] BuyCryptoDTO dto)
        {


            var user = await _context.Users
                .Include(u => u.VirtualWallet)
                .ThenInclude(w => w.CryptoAssets)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return NotFound("Felhasználó nem található");

            var crypto = await _context.CryptoCurrencies
                .FirstOrDefaultAsync(c => c.Symbol == dto.Symbol);

            if (crypto == null)
                return NotFound("Ilyen kriptovaluta nem létezik");

            double totalCost = dto.AmountToBuy * crypto.Price;

            if (user.VirtualWallet.Amount < totalCost)
                return BadRequest("Nincs elég pénz az egyenlegen hogy ennyit vegyél");

            user.VirtualWallet.Amount -= totalCost;
            if (crypto.Amount < dto.AmountToBuy)
                return BadRequest("Nincs elég elérhető mennyiség ebből a kriptovalutából");
            crypto.Amount -= dto.AmountToBuy;
            
            var existingAsset = user.VirtualWallet.CryptoAssets
                .FirstOrDefault(c => c.Symbol == dto.Symbol);

            if (existingAsset != null)
            {
                existingAsset.Amount += dto.AmountToBuy;
                existingAsset.Price += totalCost;
            }
            else
            {
                var newAsset = new CryptoAsset
                {
                    Symbol = dto.Symbol,
                    Amount = dto.AmountToBuy,
                    Price = totalCost,
                    CryptoCurrencyName = dto.CryptoCurrencyName,
                    VirtualWalletId = user.VirtualWallet.VirtualWalletId,
                };
                _context.CryptoAssets.Add(newAsset);
            }
            var logEntry = new TransactionDTO
            {
                UserId = userId,
                Symbol = dto.Symbol,
                CryptoCurrencyName = dto.CryptoCurrencyName,
                Amount = dto.AmountToBuy,
                Price = totalCost,
                Date = DateTime.UtcNow,
                Status = "buy"
            };
            _logService.AddLog(logEntry);

            await _context.SaveChangesAsync();
            return Ok("Sikeres vásárlás");

        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellCrypto(int userId,[FromBody] SellCryptoDTO dto)
            {
            var transactions = _logService.AllLogs();   
            var user = await _context.Users
               .Include(u => u.VirtualWallet)
               .ThenInclude(w => w.CryptoAssets)
               .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                    return NotFound("Felhasználó nem található");

                var crypto = await _context.CryptoCurrencies
                    .FirstOrDefaultAsync(c => c.Symbol == dto.Symbol);

            if (crypto == null)
                return NotFound("Ilyen kriptovaluta nem létezik");
            else
            {
                var cryptoAsset = user.VirtualWallet.CryptoAssets
                .FirstOrDefault(ca => ca.Symbol == dto.Symbol);

                if(cryptoAsset == null)
                    return NotFound("Nincs ilyen kriptovaluta a Walletedben");

                if (cryptoAsset.Amount < dto.AmountToSell)
                    return BadRequest("Nem tudsz ennyit eladni");
                cryptoAsset.Amount -= dto.AmountToSell;
                double totalSale = dto.AmountToSell * crypto.Price;
                cryptoAsset.Price -= totalSale;
                crypto.Amount += dto.AmountToSell;
                if (cryptoAsset.Amount == 0)
                    _context.CryptoAssets.Remove(cryptoAsset);
                user.VirtualWallet.Amount += totalSale;

                var LogEntry = new TransactionDTO
                {
                    UserId = userId,
                    Symbol = dto.Symbol,
                    CryptoCurrencyName = dto.CryptoCurrencyName,
                    Amount = dto.AmountToSell,
                    Price = totalSale,
                    Date = DateTime.UtcNow,
                    Status = "sell"
                };

                _logService.AddLog(LogEntry);

            }

            await _context.SaveChangesAsync();
            return Ok("Sikeres eladás");
            }



        [HttpGet("portfolio/{userId}")]

        public async Task<IActionResult> Portfolio(int userId)
        {
            var portfolio = await _context.VirtualWallets
            .Include(w => w.CryptoAssets)
            .FirstOrDefaultAsync(w => w.UserId == userId);

            if (portfolio == null)
                return NotFound("Nem található portfolio ezzel a user Id-vel.");

            var walletDto = mapper.Map<GetWalletDTO>(portfolio);
            return Ok(walletDto);
        }

    }
}
