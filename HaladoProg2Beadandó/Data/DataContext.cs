using Microsoft.EntityFrameworkCore;
using HaladoProg2Beadandó.Models;

namespace HaladoProg2Beadandó.Data
{
    public class DataContext : DbContext
    {
       public DbSet<User> Users { get; set; }
        public DbSet<VirtualWallet> VirtualWallets { get; set; }
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
        }
    }

}
