using AutoMapper;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs.CryptoCurrency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace HaladoProg2Beadandó.Service
{
    public interface ICryptoCurrencyService
    {
       Task<List<CryptoCurrency>> GetAllCryptosAsync();

        Task<CryptoCurrency> GetSelectedCryptoAsync(int cryptoId);

        Task<CryptoCurrency> AddCryptoAsync(AddCryptoCurrencyDTO AddcryptoCurrencyDTO);

        Task DeleteCryptoAsync(int cryptoId);

    }
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CryptoCurrencyService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CryptoCurrency>> GetAllCryptosAsync()
        {
            var cryptoList = await _context.CryptoCurrencies.ToListAsync();
            return cryptoList;
        }


        public async Task<CryptoCurrency> GetSelectedCryptoAsync(int cryptoId)
        {
            var crypto = await _context.CryptoCurrencies.FirstOrDefaultAsync(c => c.CryptoCurrencyId == cryptoId);
            if(crypto == null)
                throw new InvalidOperationException("Nincs ilyen id-jű Crypto");
            return crypto;
        }

        public async Task<CryptoCurrency> AddCryptoAsync(AddCryptoCurrencyDTO AddcryptoCurrencyDTO)
        {
            var crypto = _mapper.Map<CryptoCurrency>(AddcryptoCurrencyDTO);
            if(crypto == null)
                throw new InvalidOperationException("Hiba történt a CryptoCurrency létrehozásakor");

            var existing = await _context.CryptoCurrencies.FirstOrDefaultAsync(c => c.Symbol == crypto.Symbol || c.CryptoCurrencyName == crypto.CryptoCurrencyName);
            if (existing != null)
                throw new InvalidOperationException("Létezik ilyen kripto");

            _context.CryptoCurrencies.Add(crypto);
            await _context.SaveChangesAsync();
            return crypto;
        }

        public async Task DeleteCryptoAsync(int cryptoId)
        {
            var crypto = await _context.CryptoCurrencies.FirstOrDefaultAsync(c => c.CryptoCurrencyId == cryptoId);
            if (crypto == null)
                throw new InvalidOperationException("Nincs ilyen id-jű Crypto");
            _context.CryptoCurrencies.Remove(crypto);
            await _context.SaveChangesAsync();
        }
    }
}
