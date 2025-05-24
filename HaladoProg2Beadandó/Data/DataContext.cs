using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Entities;

namespace HaladoProg2Beadandó.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VirtualWallet> VirtualWallets { get; set; }

        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }

        public DbSet<CryptoAsset> CryptoAssets { get; set; }

        public DbSet<CryptoPriceHistory> CryptoPriceHistories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<VirtualWallet>().ToTable("VirtualWallets");
            modelBuilder.Entity<VirtualWallet>()
              .HasOne(vw => vw.User)
              .WithOne(u => u.VirtualWallet)
              .HasForeignKey<VirtualWallet>(vw => vw.UserId);


            modelBuilder.Entity<CryptoAsset>().ToTable("CryptoAssets");
            modelBuilder.Entity<CryptoAsset>()
                .HasOne(ca => ca.VirtualWallet)
                .WithMany(vw => vw.CryptoAssets)
                .HasForeignKey(ca => ca.VirtualWalletId);

            modelBuilder.Entity<CryptoCurrency>().ToTable("CryptoCurrencies");

            modelBuilder.Entity<CryptoPriceHistory>().ToTable("CryptoPriceHistories");
            modelBuilder.Entity<CryptoPriceHistory>()
                .HasOne(cph => cph.CryptoCurrency)
                .WithMany(cc => cc.PriceHistory)
                .HasForeignKey(cph => cph.CryptoCurrencyId);


            modelBuilder.Entity<Transaction>().ToTable("Transactions");
        }

    }
    }
