using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs;
using HaladoProg2Beadandó.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoPriceChangeController : DataContextController
    {

        private readonly CryptoChangeLogService _log;   
        public CryptoPriceChangeController(DataContext context, CryptoChangeLogService log) : base(context) {

            _log = log;
        }

        [HttpPut("price")]
        public async Task<IActionResult> ManualCryptoPrice([FromBody] ModifyCryptoPriceDTO dto)
        { 
            var crypto = await _context.CryptoCurrencies.FindAsync(dto.cryptoId);   
            if (crypto == null)
                return BadRequest("Nincs ilyen Idjű kriptovaluta");
            crypto.Price = dto.newPrice;
            await _context.SaveChangesAsync();
            return Ok($"Idjű {dto.cryptoId} kriptovaluta árfolyama módosult: {dto.newPrice}");
        }

        [HttpGet("price/history/{cryptoId}")]
        public async Task<IActionResult> LogCryptoChange(int cryptoId)
        {
            var cryptos = _log.AllLogs();
            if (cryptos == null || !cryptos.Any(x => x.CryptoCurrencyId == cryptoId))
                return NotFound("Nincs ilyen id-jű kriptovaluta");
            var result = cryptos.Where(x => x.CryptoCurrencyId == cryptoId).ToList();
            return Ok(result);
        }
    }
}
