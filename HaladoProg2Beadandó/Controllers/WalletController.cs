using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.Wallet;



namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : DataContextController
    {

        private readonly IMapper mapper;
        public WalletController(DataContext context, IMapper mapper) : base(context) 
            {
                this.mapper = mapper;
            }
        

        [HttpGet("{userId}")]
        public async Task<IActionResult> getWalletById(int userId)
        {
            var wallet = await _context.VirtualWallets
            .Include(w => w.CryptoAssets)
            .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
                return NotFound("Nem található pénztárca ezzel a user Id-vel.");

            var walletDto = mapper.Map<GetWalletDTO>(wallet);
            return Ok(walletDto);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> EditWalletAmount(int userId, [FromBody] WalletDTO walletDTO)
        {
            var result = await _context.VirtualWallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (result == null)
                return NotFound("Nincs ilyen idjű wallet");
            result.Amount = walletDTO.Amount;
            await _context.SaveChangesAsync();
            return Ok($"Sikeresen módosítva lett a {userId} id-jű wallet egyenlege.");
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteWallet(int userId)
        {
            var result = await _context.VirtualWallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (result == null)
                return NotFound("Nincs ilyen id-jű felhasználó.");
            _context.VirtualWallets.Remove(result);
            _context.SaveChanges();
            return Ok($"Sikeresen törölve lett a {userId} id-hez tartozó walletje.");
        }
    }
}
