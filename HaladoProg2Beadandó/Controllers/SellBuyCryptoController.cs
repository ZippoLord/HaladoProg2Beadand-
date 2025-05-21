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
using HaladoProg2Beadandó.Models.DTOs.Transaction;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/trade")]
    [ApiController]
    public class SellBuyCryptoController : ControllerBase
    {
       private readonly ISellBuyCryptoService _sellBuyCryptoService;

        public SellBuyCryptoController(ISellBuyCryptoService sellBuyCryptoService)
        {
            _sellBuyCryptoService = sellBuyCryptoService;
        }



        [HttpPost("buy")]
        public async Task<IActionResult> BuyCrypto(int userId, [FromBody] BuyCryptoDTO dto)
        {
            try {

                await _sellBuyCryptoService.BuyCrypto(userId, dto);
                return Ok($"Sikeres vásárlás");
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellCrypto(int userId, [FromBody] SellCryptoDTO dto)
        {
            try { 
                await _sellBuyCryptoService.SellCrypto(userId, dto);
                return Ok("Sikeres eladás");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("portfolio/{userId}")]

        public async Task<IActionResult> Portfolio(int userId)
        {
            try { 
                var result = await _sellBuyCryptoService.getPortfolio(userId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
