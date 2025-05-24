using HaladoProg2Beadandó.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HaladoProg2Beadandó.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.CryptoCurrency;
using HaladoProg2Beadandó.Service;
using HaladoProg2Beadandó.Models.DTOs.User;

namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CryptoCurrencyController : ControllerBase
    {
        
        private readonly ICryptoCurrencyService _crypto;

        public CryptoCurrencyController(ICryptoCurrencyService crypto)
        {
            _crypto = crypto;
        }

        [HttpGet("cryptos")]
        public async Task<IActionResult> GetAllCrypto()
        {
            try
            {
                var result = await _crypto.GetAllCryptosAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt: {ex.Message}");
            }
        }

        [HttpGet("/cryptos/{cryptoId}")]

        public async Task<IActionResult> GetaCryptoById(int cryptoId)
        {
            try
            {
                var result = await _crypto.GetSelectedCryptoAsync(cryptoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt: {ex.Message}");
            }
        }

        [HttpPost("cryptos")]

        public async Task<IActionResult> AddCrypto([FromBody] AddCryptoCurrencyDTO AddcryptoCurrencyDTO)
        {
            try
            {
                var result = await _crypto.AddCryptoAsync(AddcryptoCurrencyDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt: {ex.Message}");
            }
        }

        [HttpDelete("cryptos/{cryptoId}")]

        public async Task<IActionResult> DeleteCryptoById(int cryptoId)
        {
            try
            {
                await _crypto.DeleteCryptoAsync(cryptoId);
                return Ok("Sikeresen törölted a kriptot");
            }
            catch (Exception ex)
            {
               return StatusCode(500, $"Hiba történt: {ex.Message}");
            }
        }


    }
}
