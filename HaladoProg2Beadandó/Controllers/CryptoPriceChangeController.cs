using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs.CryptoPrice;
using HaladoProg2Beadandó.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoPriceChangeController : ControllerBase
    {

        private readonly ICryptoPriceChange _cryptoPriceChange;
        public CryptoPriceChangeController(ICryptoPriceChange cryptoPriceChange)
        {
            _cryptoPriceChange = cryptoPriceChange;
        }

        [HttpPut("price")]
        public async Task<IActionResult> ManualCrypto(int cryptoId, [FromBody] ModifyCryptoPriceDTO dto)
        {

            try
            {

                var result = await _cryptoPriceChange.ManualCryptoPrice(cryptoId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba történt: {ex.Message}");
            }
        }

        [HttpGet("price/history/{cryptoId}")]
        public async Task<IActionResult> LogCryptoChange(int cryptoId)
        {
            try
            {
                var result = await _cryptoPriceChange.HistoryCryptoPricesAsync(cryptoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba történt: {ex.Message}");
            }
        }
    }
}
