using AutoMapper;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs.BuyAndSell;
using HaladoProg2Beadandó.Models.DTOs.Transaction;
using HaladoProg2Beadandó.Models.DTOs.Wallet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaladoProg2Beadandó.Services
{

    public interface ISellBuyCryptoService
    {
        Task BuyCrypto(int userId, BuyCryptoDTO BuyCryptoDTO);

        Task SellCrypto(int userId, SellCryptoDTO SellCryptoDTO);

        Task<PortfolioDTO> getPortfolio(int userId);
    }

    public class SellBuyCryptoService : ISellBuyCryptoService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
       
        public SellBuyCryptoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task BuyCrypto(int userId, BuyCryptoDTO BuyCryptoDTO)
        {

            var user = await _context.Users
                .Include(u => u.VirtualWallet)
                .ThenInclude(w => w.CryptoAssets)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                throw new InvalidOperationException("Felhasználó nem található");

            var crypto = await _context.CryptoCurrencies
                .FirstOrDefaultAsync(c => c.Symbol == BuyCryptoDTO.Symbol && c.CryptoCurrencyName == BuyCryptoDTO.CryptoCurrencyName);
            if (crypto == null)
                throw new InvalidOperationException($"Ilyen kriptovaluta nem létezik {crypto}");

            double totalCost = BuyCryptoDTO.AmountToBuy * crypto.Price;

            if (user.VirtualWallet.Amount < totalCost)
                throw new InvalidOperationException("Nincs elég pénz az egyenlegen hogy ennyit vegyél");

            if (crypto.Amount < BuyCryptoDTO.AmountToBuy)
                throw new InvalidOperationException("Nincs elég elérhető mennyiség ebből a kriptovalutából");

            user.VirtualWallet.Amount -= totalCost;
            crypto.Amount -= BuyCryptoDTO.AmountToBuy;

            var existingAsset = user.VirtualWallet.CryptoAssets
                .FirstOrDefault(c => c.Symbol == BuyCryptoDTO.Symbol);

            if (existingAsset != null)
            {
                existingAsset.Amount += BuyCryptoDTO.AmountToBuy;
                existingAsset.Price += totalCost;
            }
            else
            {
                var newAsset = new CryptoAsset
                {
                    Symbol = BuyCryptoDTO.Symbol,
                    Amount = BuyCryptoDTO.AmountToBuy,
                    Price = totalCost,
                    CryptoCurrencyName = BuyCryptoDTO.CryptoCurrencyName,
                    VirtualWalletId = user.VirtualWallet.VirtualWalletId,
                };
                _context.CryptoAssets.Add(newAsset);
            }


            var list = _context.Transactions.Add(
                new Transaction
                { 
                    UserId = userId,
                    Symbol = BuyCryptoDTO.Symbol,
                    CryptoCurrencyName = BuyCryptoDTO.CryptoCurrencyName,
                    Amount = BuyCryptoDTO.AmountToBuy,
                    Price = totalCost,
                    Date = DateTime.UtcNow,
                    Status = "buy"
                });

            await _context.SaveChangesAsync();
        }


        public async Task SellCrypto(int userId, SellCryptoDTO dto)
        {
            var user = await _context.Users
               .Include(u => u.VirtualWallet)
               .ThenInclude(w => w.CryptoAssets)
               .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                throw new InvalidOperationException("Felhasználó nem található");

            var crypto = await _context.CryptoCurrencies
                .FirstOrDefaultAsync(c => c.Symbol == dto.Symbol && c.CryptoCurrencyName == dto.CryptoCurrencyName);

            if (crypto == null)
                throw new InvalidOperationException("Ilyen kriptovaluta nem létezik");
            var cryptoAsset = user.VirtualWallet.CryptoAssets
            .FirstOrDefault(ca => ca.Symbol == dto.Symbol);

            if (cryptoAsset == null)
                throw new InvalidOperationException("Nincs ilyen kriptovaluta a pénztárcádban");

            if (cryptoAsset.Amount < dto.AmountToSell)
                throw new InvalidOperationException("Nem tudsz ennyit eladni");


            //levondom amennyit szeretnék
            cryptoAsset.Amount -= dto.AmountToSell;

            //kiszámolom az eladás alapján hogy mennyit kapok
            double totalSale = dto.AmountToSell * crypto.Price;


            cryptoAsset.Price -= totalSale;

            //visszaadom a kripto táblába a mennyiséget 
            crypto.Amount += dto.AmountToSell;

            //ha a kriptovaluta mennyisége 0-ra csökkent, akkor törlöm a CryptoAsset-t
            if (cryptoAsset.Amount == 0)
                _context.CryptoAssets.Remove(cryptoAsset);

            //hozzáadom a kikalkulát árat
            user.VirtualWallet.Amount += totalSale;



            var transaction = _context.Transactions.Add(
                  new Transaction
                  {
                      UserId = userId,
                      Symbol = dto.Symbol,
                      CryptoCurrencyName = dto.CryptoCurrencyName,
                      Amount = dto.AmountToSell,
                      Price = totalSale,
                      Date = DateTime.UtcNow,
                      Status = "sell"
                  });

            await _context.SaveChangesAsync();
        }
    


        public async Task<PortfolioDTO> getPortfolio(int userId)
        {
            var user = await _context.Users.
                Include(u => u.VirtualWallet)
            .ThenInclude(w => w.CryptoAssets)
            .FirstOrDefaultAsync(w => w.UserId == userId);

            if (user == null)
                throw new InvalidOperationException("A felhasználó nem található.");


            if (user.VirtualWallet == null)
                throw new InvalidOperationException("A felhasználónak nincs pénztárcája.");

            var portfolio = new PortfolioDTO
            {
                WalletPrice = Math.Round(user.VirtualWallet.Amount, 2),
                CryptoAssets = new List<PortfolioItemDTO>()
            };

            foreach (var asset in user.VirtualWallet.CryptoAssets)
            {
                var crypto = await _context.CryptoCurrencies
                    .FirstOrDefaultAsync(c => c.Symbol == asset.Symbol && c.CryptoCurrencyName == asset.CryptoCurrencyName);

                var item = new PortfolioItemDTO
                {
                    Symbol = asset.Symbol,
                    CryptoCurrencyName = asset.CryptoCurrencyName,
                    Amount = asset.Amount,
                    PurchaseValue = Math.Round(asset.Price, 2),
                    CurrentPricePerUnit = crypto.Price,
                    CurrentValue = Math.Round(crypto.Price * asset.Amount, 2)
                };
                
                portfolio.CryptoAssets.Add(item);
            }
            return portfolio;
        }
    }
}
