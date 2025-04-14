using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models.DTOs;



namespace HaladoProg2Beadandó.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : DataContextController
    {
        public WalletController(DataContext context) : base(context) { }

        [HttpGet("{userId}")]
        public async Task<IActionResult> getWalletById(int userId)
        {
            var result = await _context.VirtualWallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (result == null)
                return new JsonResult(NotFound());
            var uservallet = await _context.Users.Where(u => u.UserId == userId).Select(u => new
            {
                VirtualWallet = new
                {
                    u.VirtualWallet.Amount,
                    CryptoAssets = u.VirtualWallet.CryptoAssets.Select(ca => new
                    { ca.Symbol, ca.Amount, ca.CryptoCurrencyName, ca.Price }).ToList()
                }
            }).FirstOrDefaultAsync();
            return Ok(uservallet);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> EditWalletAmount(int userId, [FromBody] WalletDTO walletDTO)
        {
            var result = await _context.VirtualWallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (result == null)
                return new JsonResult(NotFound("Nincs ilyen idjű wallet"));
            result.Amount = walletDTO.Amount;
            await _context.SaveChangesAsync();
            return new JsonResult(Ok($"Sikeresen módosítva lett a {userId} id-jű wallet egyenlege."));
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteWallet(int userId)
        {
            var result = await _context.VirtualWallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (result == null)
                return new JsonResult(NotFound("Nincs ilyen id-jű felhasználó."));
            _context.VirtualWallets.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok($"Sikeresen törölve lett a {userId} id-hez tartozó walletje."));
        }
    }
}
