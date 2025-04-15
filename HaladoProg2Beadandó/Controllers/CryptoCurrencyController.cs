using HaladoProg2Beadandó.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using AutoMapper;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/crypto")]
    public class CryptoCurrencyController : DataContextController
    {
        private readonly IMapper mapper;
        public CryptoCurrencyController(DataContext context, IMapper mapper) : base(context) {
            this.mapper = mapper;
        }


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

        public async Task<IActionResult> AddCrypto([FromBody] CryptoCurrencyDTO cryptoCurrencyDTO)
        {
            var crypto = mapper.Map<CryptoCurrency>(cryptoCurrencyDTO);
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
