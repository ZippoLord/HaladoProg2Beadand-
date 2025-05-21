using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.Wallet;
using HaladoProg2Beadandó.Services;
using HaladoProg2Beadandó.Models.DTOs.User;



namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {

        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService) 
            {
                 _walletService = walletService;
            }
        

        [HttpGet("{userId}")]
        public async Task<ActionResult> getWalletById(int userId)
        {
            try
            {
                var result = await _walletService.getWallet(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> EditWalletAmount(int userId, editAmoutDTO walletDTO)
        {
            try
            {
                var result = await _walletService.EditWalletAmount(userId, walletDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteWallet(int userId)
        {
                try
                {
                    await _walletService.DeleteWalletByAsync(userId);
                    return Ok("Sikeresen törölted a pénztárcát");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        }
    }
}
