using AutoMapper;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs;
using HaladoProg2Beadandó.Models.DTOs.Wallet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HaladoProg2Beadandó.Services
{

    public interface IWalletService{
        public Task<GetWalletDTO> getWallet(int userId);

        public Task<GetWalletDTO> EditWalletAmount(int userId, editAmoutDTO walletDTO);

        public Task DeleteWalletByAsync(int userId);
    }
    public class WalletService : IWalletService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WalletService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<GetWalletDTO> getWallet(int userId)
        {
            var wallet = await _context.VirtualWallets.Include(c => c.CryptoAssets).FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
            {
                throw new InvalidOperationException("Nincs ilyen id-jű pénztárca");
            }

            var walletDTO = new GetWalletDTO
            {
                Amount = wallet.Amount,
                CryptoAssets = wallet.CryptoAssets.Select(c => new CryptoAssetDTO
                {
                    CryptoCurrencyName = c.CryptoCurrencyName,
                    Symbol = c.Symbol,
                    Price = c.Price,
                    Amount = c.Amount
                }).ToList()
            };
            return walletDTO;
        }

        public async Task<GetWalletDTO> EditWalletAmount(int userId, editAmoutDTO walletDTO)
        {
            var wallet = await _context.VirtualWallets
                .Include(c => c.CryptoAssets)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
            {
                throw new InvalidOperationException("Nincs ilyen id-jű pénztárca");
            }

            _mapper.Map(walletDTO, wallet);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWalletDTO>(wallet);
        }

        public async Task DeleteWalletByAsync(int userId)
        {
            var wallet = await _context.VirtualWallets
                .Include(c => c.CryptoAssets)
                .FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
            {
                throw new InvalidOperationException("Nincs ennek a usernek pénztárca");
            }
            _context.VirtualWallets.Remove(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
