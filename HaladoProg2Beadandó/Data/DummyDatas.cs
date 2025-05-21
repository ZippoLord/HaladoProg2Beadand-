using HaladoProg2Beadandó.Controllers;
using HaladoProg2Beadandó.Migrations;
using HaladoProg2Beadandó.Models;
using Microsoft.EntityFrameworkCore;

namespace HaladoProg2Beadandó.Data
{
    public class DummyDatas : DataContextController
    {
        public DummyDatas(DataContext context) : base(context)
        {
        }
        public static void SeedDatabase(DataContext context) 
        {
            if (context.Users.Any() || context.CryptoCurrencies.Any())
                return; 

            var cryptos = new List<CryptoCurrency>
        {
             new CryptoCurrency { Symbol = "BTC", CryptoCurrencyName = "Bitcoin", Price = 29200.50, Amount = 1500 },
                new CryptoCurrency { Symbol = "ETH", CryptoCurrencyName = "Ethereum", Price = 1800.75, Amount = 1200 },
                new CryptoCurrency { Symbol = "USDT", CryptoCurrencyName = "Tether", Price = 1.00, Amount = 500000 },
                new CryptoCurrency { Symbol = "BNB", CryptoCurrencyName = "Binance Coin", Price = 430.00, Amount = 1800 },
                new CryptoCurrency { Symbol = "SOL", CryptoCurrencyName = "Solana", Price = 150.25, Amount = 1200 },
                new CryptoCurrency { Symbol = "XRP", CryptoCurrencyName = "Ripple", Price = 1.50, Amount = 3000 },
                new CryptoCurrency { Symbol = "ADA", CryptoCurrencyName = "Cardano", Price = 0.90, Amount = 3500 },
                new CryptoCurrency { Symbol = "DOGE", CryptoCurrencyName = "Dodgecoin", Price = 0.10, Amount = 4500 },
                new CryptoCurrency { Symbol = "DOT", CryptoCurrencyName = "Polkadot", Price = 40.00, Amount = 1600 },
                new CryptoCurrency { Symbol = "AVAX", CryptoCurrencyName = "Avalanche", Price = 80.00, Amount = 1000 },
                new CryptoCurrency { Symbol = "TRX", CryptoCurrencyName = "Tron", Price = 0.07, Amount = 2500 },
                new CryptoCurrency { Symbol = "LTC", CryptoCurrencyName = "Litecoin", Price = 200.00, Amount = 1700 },
                new CryptoCurrency { Symbol = "LINK", CryptoCurrencyName = "Chainlink", Price = 25.00, Amount = 2100 },
                new CryptoCurrency { Symbol = "SHIB", CryptoCurrencyName = "Shiba Inu", Price = 0.10, Amount = 1000 },
                new CryptoCurrency { Symbol = "UNI", CryptoCurrencyName = "Uniswap", Price = 15.00, Amount = 1300 }
        };
            context.CryptoCurrencies.AddRange(cryptos);

            var users = new List<User>
        {
            new User
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "hashed_pw_1", 
                VirtualWallet = new VirtualWallet
                {
                    Amount = 10000,
                    CryptoAssets = new List<CryptoAsset>
                    {
                        new CryptoAsset
                        {
                            Symbol = "BTC",
                            Amount = 0.25,
                            Price = 7500,
                            CryptoCurrencyName = "Bitcoin"
                        },
                        new CryptoAsset
                        {
                            Symbol = "ETH",
                            Amount = 1.5,
                            Price = 2700,
                            CryptoCurrencyName = "Ethereum"
                        }
                    }
                }
            },
            new User
            {
                Name = "Jane Smith",
                Email = "jane@example.com",
                Password = "hashed_pw_2",
                VirtualWallet = new VirtualWallet
                {
                    Amount = 5000,
                    CryptoAssets = new List<CryptoAsset>
                    {
                        new CryptoAsset
                        {
                            Symbol = "SOL",
                            Amount = 10,
                            Price = 1400,
                            CryptoCurrencyName = "Solana"
                        }
                    }
                }
            }
        };
            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }

}


