using HaladoProg2Beadandó.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/crypto")]
    public class CryptoCurrencyController : DataContextController
    {
        public CryptoCurrencyController(DataContext context) : base(context) { }


        [HttpGet("cryptos")]
        public async Task<ActionResult<CryptoCurrency>> GetAllCrypto()

        {
            var result = await _context.CryptoCurrencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{cryptoId}")]

        public async Task<ActionResult<CryptoCurrency>> GetaCryptoById(int cryptoId)
        {
            var result = await _context.CryptoCurrencies.FindAsync(cryptoId);
            if(result == null) 
                return NotFound($"Nincs ilyen id-jű kriptovaluta {cryptoId}");
            return result;
        }



        [HttpPut("cryptos")]

        public async Task<ActionResult> AddCrypto([FromBody] CryptoCurrencyDTO cryptoCurrencyDTO)
        {
            var crypto = new CryptoCurrency
            {
                Symbol = cryptoCurrencyDTO.Symbol,
                CryptoCurrencyName = cryptoCurrencyDTO.CryptoCurrencyName,
                Price = cryptoCurrencyDTO.Price,
                Amount = cryptoCurrencyDTO.Amount
            };
            _context.CryptoCurrencies.Add(crypto);
            await _context.SaveChangesAsync();
            return Ok("Sikeresen hozzáadta cryptovalutát");
        }


        [HttpDelete("{cryptoId}")]

        public  async Task<ActionResult> DeleteCryptoById(int cryptoId)
        {
            var result =  await _context.CryptoCurrencies.FindAsync(cryptoId);
            if (result == null)
                return NotFound($"Nincs ilyen id-jű kriptovaluta {cryptoId}");
            _context.CryptoCurrencies.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Sikeresen törölve lett a {cryptoId} id-jű crypto.");
        }


    }
}
