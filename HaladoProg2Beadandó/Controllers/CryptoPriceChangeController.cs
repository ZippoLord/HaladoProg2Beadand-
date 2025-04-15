using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoPriceChangeController : DataContextController
    {

        public CryptoPriceChangeController(DataContext context) : base(context) { }

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
        public async Task<IActionResult> LogCryptoChange()
        {

        }
    }
}
